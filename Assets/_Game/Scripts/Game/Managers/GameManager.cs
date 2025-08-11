using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Ins;
    void Awake()
    {
        Ins = this;
    }
    GameState currentState;

    public bool IsState(GameState state) => currentState == state;
    public void ChangeState(GameState newState)
    {
        currentState = newState;
    }
}

public enum GameState
{
    MAIN_MENU,
    GAME_PLAY,
    SETTING,
    WIN
}