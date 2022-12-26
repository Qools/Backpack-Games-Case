using System;

public static class EventSystem
{
    public static Action OnStartGame;
    public static void CallStartGame() => OnStartGame?.Invoke();

    public static Action<Tagging> OnNpcTag;
    public static void CallTagNpc(Tagging tagged) => OnNpcTag?.Invoke(tagged);

    public static Action<GameResult> OnGameOver;
    public static void CallGameOver(GameResult gameResult) => OnGameOver?.Invoke(gameResult);

    public static Action<TargetObject> OnObjectTaken;
    public static void CallObjectTaken(TargetObject targetObject) => OnObjectTaken?.Invoke(targetObject);

    public static Action OnNewLevelLoad;
    public static void CallNewLevelLoad() => OnNewLevelLoad?.Invoke();
}
