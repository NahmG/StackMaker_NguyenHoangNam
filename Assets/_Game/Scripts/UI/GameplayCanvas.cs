using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

using URandom = UnityEngine.Random;

public class GameplayCanvas : UICanvas
{
    public override UI ID => UI.GAME_PLAY;

    [SerializeField] TMP_Text levelText;
    [SerializeField] Button settingButton;

    [Header("Gem Anim")]
    [SerializeField] Transform gemTextSpawnTf;
    [SerializeField] GameObject gemAddText;
    [SerializeField] Transform gemSpawnTf;
    [SerializeField] GameObject UIGemPref;
    [SerializeField] TMP_Text gemCounter;
    int currentGemCount;

    Action action;

    void Awake()
    {
        settingButton.onClick.AddListener(OnSettingBtnClick);
        UIManager.Ins.PreloadUI(UI.SETTING);

        GameplayManager.Ins.OnUpdateGemCount += OnUpdateGemCount;
    }

    void OnDestroy()
    {
        settingButton.onClick.RemoveListener(OnSettingBtnClick);
        GameplayManager.Ins.OnUpdateGemCount -= OnUpdateGemCount;
    }

    public override void Setup()
    {
        base.Setup();
        levelText.text = LevelManager.Ins.Level.ToString();
    }

    public override void Open(object param = null)
    {
        base.Open(param);

        if (param is Action)
        {
            this.action = param as Action;
        }

        action?.Invoke();
    }

    public void OnSettingBtnClick()
    {
        UIManager.Ins.OpenUI(UI.SETTING);
    }

    public void OnUpdateGemCount(int value)
    {
        AnimateGems();
        ShowGemAddText(value);
    }

    void AnimateGems()
    {
        int spawnCount = 6;
        float radius = 200f;
        for (int i = 0; i < spawnCount; i++)
        {
            //Spawn gem
            Transform gem = Instantiate(UIGemPref, gemSpawnTf).transform;

            //move to random pos    
            Vector2 randomPos = (Vector2)gemSpawnTf.position + URandom.insideUnitCircle * radius;

            Sequence seq = DOTween.Sequence();

            seq.Append(gem.DOMove(randomPos, .5f)
            .SetEase(Ease.OutQuad));

            //fly toward gemCounter
            seq.Append(gem.DOMove(gemCounter.transform.position, .5f)
            .SetDelay(.5f)
            .SetEase(Ease.InQuad)
            .OnComplete(() =>
            {
                Destroy(gem.gameObject);
                UpdateGemCounter();
            }));

            seq.SetAutoKill(true);
        }
    }

    void ShowGemAddText(int value)
    {
        Transform gemText = Instantiate(gemAddText, gemTextSpawnTf).transform;
        gemText.GetComponent<Text>().text = $"+{value}";
        gemText.localScale = Vector3.zero;
        Sequence seq = DOTween.Sequence();

        seq.Append(gemText.DOScale(1, .5f)
        .SetEase(Ease.OutBack));

        seq.Append(gemText.DOScale(0, .3f)
        .SetDelay(.5f)
        .SetEase(Ease.InBack)
        .OnComplete(() =>
        {
            Destroy(gemText.gameObject);
        }));

        seq.SetAutoKill(true);
    }

    void UpdateGemCounter()
    {
        DOVirtual.Int(currentGemCount, GameplayManager.Ins.Gem, .1f, value =>
        {
            currentGemCount = value;
            gemCounter.text = $"{value}";
        });
    }
}