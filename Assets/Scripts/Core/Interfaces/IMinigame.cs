/// <summary>
/// ミニゲームの共通インターフェース
/// 新しいミニゲームを追加するときはこれを実装する
/// </summary>
public interface IMinigame
{
    /// <summary>ゲーム開始</summary>
    void StartGame();

    /// <summary>ゲーム終了</summary>
    void EndGame();

    /// <summary>ゲームの更新処理</summary>
    void UpdateGame();

    /// <summary>ゲームがプレイ中かどうか</summary>
    bool IsPlaying { get; }
}