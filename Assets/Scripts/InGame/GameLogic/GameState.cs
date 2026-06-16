/// <summary>
/// ゲームの状態を管理するEnum
/// Stateパターンの状態定義
/// </summary>
public enum GameState
{
    /// <summary>待機中（ゲーム開始前）</summary>
    Idle,

    /// <summary>カウントダウン中</summary>
    Countdown,

    /// <summary>プレイ中</summary>
    Playing,

    /// <summary>一時停止中</summary>
    Paused,

    /// <summary>ゲーム終了</summary>
    Result
}