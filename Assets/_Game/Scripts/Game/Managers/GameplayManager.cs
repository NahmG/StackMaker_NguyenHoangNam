using System;
using DG.Tweening;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    public static GameplayManager Ins;
    [SerializeField] Player playerPref;
    [SerializeField] CameraFollow mainCam;
    public int brickCount;
    Player player;
    public Player Player => player;
    Level currentLevel => LevelManager.Ins.CurrentLevel;

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
        ConstructLevel();
        GameManager.Ins.ChangeState(GameState.GAME_PLAY);
    }

    public void Win()
    {
        DOVirtual.DelayedCall(1.5f, OnWin);

        void OnWin()
        {
            UIManager.Ins.CloseAll();
            UIManager.Ins.OpenUI(UI.WIN);
            GameManager.Ins.ChangeState(GameState.WIN);
        }
    }

    void ConstructLevel()
    {
        LevelManager.Ins.LoadLevel();
        if (player == null)
        {
            player = Instantiate(playerPref);
        }
        player.OnInit(currentLevel.startPosition);
        mainCam.SetTarget(player.SkinTF);
    }

    #region GAME_PARAMETER
    int gem;
    public int Gem => gem;
    public Action OnUpdateGemCount;

    public void AddDiamond(int value)
    {
        gem += value;
        OnUpdateGemCount?.Invoke();
    }
    #endregion
}