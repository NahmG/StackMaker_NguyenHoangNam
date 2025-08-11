using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WinPopup : UICanvas
{
    public override UI ID => UI.WIN;

    [SerializeField] TMP_Text brickCount;
    [SerializeField] Button nextLevel;
    [SerializeField] Button replayButton;

    Action loadingAction;

    void Awake()
    {
        nextLevel.onClick.AddListener(OnNextLevelBtnClick);
        replayButton.onClick.AddListener(OnReplayBtnClick);

        loadingAction = () =>
        {
            UIManager.Ins.CloseUI(UI.WIN);
            UIManager.Ins.OpenUI(UI.GAME_PLAY);
            GameplayManager.Ins.StartLevel();
        };
    }

    public override void Setup()
    {
        base.Setup();
        brickCount.text = GameplayManager.Ins.brickCount.ToString();
    }

    public void OnNextLevelBtnClick()
    {
        LevelManager.Ins.NextLevel();
        UIManager.Ins.OpenUI(UI.LOADING, loadingAction);
    }

    public void OnReplayBtnClick()
    {
        UIManager.Ins.OpenUI(UI.LOADING, loadingAction);
    }
}