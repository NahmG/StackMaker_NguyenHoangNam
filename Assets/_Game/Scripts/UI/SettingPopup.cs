using UnityEngine;
using UnityEngine.UI;

public class SettingPopup : UICanvas
{
    public override UI ID => UI.SETTING;

    [SerializeField] Button closeButton;

    void Awake()
    {
        closeButton.onClick.AddListener(OnCloseButtonClick);
    }

    void OnDestroy()
    {
        closeButton.onClick.RemoveListener(OnCloseButtonClick);
    }

    public override void Open(object param = null)
    {
        base.Open(param);
        GameManager.Ins.ChangeState(GameState.SETTING);
    }

    public void OnCloseButtonClick()
    {
        Close();
        GameManager.Ins.ChangeState(GameState.GAME_PLAY);
    }
}