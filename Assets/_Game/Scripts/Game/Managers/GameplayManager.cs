using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    public static GameplayManager Ins;
    [SerializeField] Player player;
    public int brickCount;

    void Awake()
    {
        Ins = this;
    }
    void Start()
    {
        LevelManager.Ins.OnInit();
        UIManager.Ins.OpenUI(UI.MAIN_MENU);
        GameManager.Ins.ChangeState(GameState.MAIN_MENU);
    }

    public void StartLevel()
    {
        Debug.Log($"Load Level {LevelManager.Ins.Level}");
        LevelManager.Ins.LoadLevel();
        player.OnInit(LevelManager.Ins.CurrentLevel.startPosition);
        GameManager.Ins.ChangeState(GameState.GAME_PLAY);
    }

    public void Win()
    {
        UIManager.Ins.CloseAll();
        UIManager.Ins.OpenUI(UI.WIN);
        GameManager.Ins.ChangeState(GameState.WIN);
    }
}