using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameplayCanvas : UICanvas
{
    public override UI ID => UI.GAME_PLAY;

    [SerializeField] TMP_Text levelText;
    [SerializeField] Button settingButton;


    void Awake()
    {
        settingButton.onClick.AddListener(OnSettingBtnClick);
        UIManager.Ins.PreloadUI(UI.SETTING);
    }

    void Update()
    {
        levelText.text = LevelManager.Ins.Level.ToString();
    }

    public void OnSettingBtnClick()
    {
        UIManager.Ins.OpenUI(UI.SETTING);
    }
}