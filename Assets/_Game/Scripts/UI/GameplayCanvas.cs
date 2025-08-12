using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameplayCanvas : UICanvas
{
    public override UI ID => UI.GAME_PLAY;

    [SerializeField] TMP_Text levelText;
    [SerializeField] Button settingButton;

    [Header("Diamond Anim")]
    [SerializeField] TMP_Text gemAddText;
    [SerializeField] GameObject UIGemPref;
    [SerializeField] TMP_Text gemCounter;
    int currentGemCount;


    void Awake()
    {
        settingButton.onClick.AddListener(OnSettingBtnClick);
        UIManager.Ins.PreloadUI(UI.SETTING);

        GameplayManager.Ins.OnUpdateGemCount += OnUpdateGemCount;
    }

    void Update()
    {
        levelText.text = LevelManager.Ins.Level.ToString();
    }

    public void OnSettingBtnClick()
    {
        UIManager.Ins.OpenUI(UI.SETTING);
    }

    public void OnUpdateGemCount()
    {
        AnimateGems();
        ShowGemAddText();

        //update gem counter
        DOVirtual.Int(currentGemCount, GameplayManager.Ins.Gem, .1f, value =>
        {
            currentGemCount = value;
            gemCounter.text = $"{value}";
        });
    }

    void AnimateGems()
    {

    }

    void ShowGemAddText()
    {

    }
}