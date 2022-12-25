using System;

public static class EventSystem
{
    public static Action OnStartGame;
    public static void CallStartGame() => OnStartGame?.Invoke();

    public static Action OnNpcTag;
    public static void CallTagNpc() => OnNpcTag?.Invoke();

    public static Action OnNpcReachTarget;
    public static void CallNpcReachTarget() => OnNpcReachTarget?.Invoke();

    public static Action<GameResult> OnGameOver;
    public static void CallGameOver(GameResult gameResult) => OnGameOver?.Invoke(gameResult);
}
