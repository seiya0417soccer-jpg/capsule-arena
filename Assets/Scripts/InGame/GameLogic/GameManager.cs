using UnityEngine;
using R3;

/// <summary>
/// ゲーム全体の進行を管理するクラス
/// IMinigameを受け取ってゲームを進行させる
/// </summary>
public class GameManager : MonoBehaviour
{
    // 現在のゲーム状態をR3で管理（変化を外に通知できる）
    public ReactiveProperty<GameState> CurrentState { get; }
        = new ReactiveProperty<GameState>(GameState.Idle);

    // 現在プレイ中のミニゲーム
    private IMinigame _currentMinigame;

    /// <summary>
    /// ミニゲームをセットして開始する
    /// Strategyパターンの考え方で差し替え可能
    /// </summary>
    public void StartMinigame(IMinigame minigame)
    {
        _currentMinigame = minigame;
        CurrentState.Value = GameState.Countdown;
        _currentMinigame.StartGame();
        CurrentState.Value = GameState.Playing;
    }

    /// <summary>
    /// ゲームを終了する
    /// </summary>
    public void EndGame()
    {
        if (_currentMinigame == null) return;
        _currentMinigame.EndGame();
        CurrentState.Value = GameState.Result;
    }

    /// <summary>
    /// 一時停止
    /// </summary>
    public void PauseGame()
    {
        if (CurrentState.Value != GameState.Playing) return;
        CurrentState.Value = GameState.Paused;
    }

    /// <summary>
    /// 再開
    /// </summary>
    public void ResumeGame()
    {
        if (CurrentState.Value != GameState.Paused) return;
        CurrentState.Value = GameState.Playing;
    }

    private void Update()
    {
        // プレイ中のみUpdateGameを呼ぶ
        if (CurrentState.Value == GameState.Playing)
        {
            _currentMinigame?.UpdateGame();
        }
    }
}