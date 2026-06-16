using UnityEngine;
using R3;

/// <summary>
/// ゴロゴロコロシアムのゲームロジック
/// IMinigameを実装することで将来的に他のミニゲームと差し替え可能
/// </summary>
public class CapsuleArenaGame : MonoBehaviour, IMinigame
{
    [SerializeField] private CapsulePlayer _player1; // プレイヤー1
    [SerializeField] private CapsulePlayer _player2; // プレイヤー2
    [SerializeField] private ArenaStage _stage;       // ステージ

    // ゲームがプレイ中かどうか
    public bool IsPlaying { get; private set; }

    // 勝者の名前をR3で管理（UIに通知できる）
    public ReactiveProperty<string> Winner { get; }
        = new ReactiveProperty<string>("");

    /// <summary>
    /// ゲーム開始処理
    /// </summary>
    public void StartGame()
    {
        IsPlaying = true;
        Winner.Value = "";
        _stage.ResetStage();
        Debug.Log("ゲーム開始！");

        // プレイヤーの死亡を監視してゲームオーバーを判定
        _player1.IsAlive.Subscribe(isAlive =>
        {
            if (!isAlive) OnPlayerDied(_player2.gameObject.name);
        }).AddTo(this);

        _player2.IsAlive.Subscribe(isAlive =>
        {
            if (!isAlive) OnPlayerDied(_player1.gameObject.name);
        }).AddTo(this);
    }

    /// <summary>
    /// ゲーム終了処理
    /// </summary>
    public void EndGame()
    {
        IsPlaying = false;
        Debug.Log("ゲーム終了！");
    }

    /// <summary>
    /// 毎フレーム呼ばれる更新処理
    /// </summary>
    public void UpdateGame()
    {
        // 将来的にここに時間管理などを追加する
    }

    /// <summary>
    /// プレイヤーが落下したときの処理
    /// </summary>
    private void OnPlayerDied(string winnerName)
    {
        if (!IsPlaying) return;
        Winner.Value = winnerName;
        EndGame();
        Debug.Log($"勝者：{winnerName}");
    }
}