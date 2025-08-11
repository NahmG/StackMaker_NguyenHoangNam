using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuCanvas : UICanvas
{
    public override UI ID => UI.MAIN_MENU;

    [SerializeField] Button playButton;
    [SerializeField] Image progressBar;
    [SerializeField] TMP_Text progressText;
    [SerializeField] float loadTime;

    Action loadingAction;

    void Awake()
    {
        playButton.onClick.AddListener(OnPlayBtnClick);
        UIManager.Ins.PreloadUI(UI.LOADING);

        loadingAction = () =>
        {
            UIManager.Ins.CloseUI(UI.MAIN_MENU);
            UIManager.Ins.OpenUI(UI.GAME_PLAY);
            GameplayManager.Ins.StartLevel();
        };
    }

    void Update()
    {
        startTime += Time.deltaTime;
        float rate = Mathf.Clamp01(startTime / loadTime);

        progressBar.fillAmount = Mathf.Lerp(0, 1, rate);

        int value = (int)Mathf.Lerp(0, 100, rate);
        progressText.text = $"{value}%";

        if (startTime >= loadTime)
        {
            AnimPlayBtnFadeIn();
        }
    }

    public override void Setup()
    {
        base.Setup();

        //turn off play button
        playButton.GetComponent<CanvasGroup>().alpha = 0;
        playButton.transform.localScale = Vector3.zero;
    }

    public void OnPlayBtnClick()
    {
        UIManager.Ins.OpenUI(UI.LOADING, loadingAction);
    }

    void AnimPlayBtnFadeIn()
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(playButton.GetComponent<CanvasGroup>().DOFade(1, .5f));
        seq.Join(playButton.transform.DOScale(1, .5f));

        seq.SetEase(Ease.OutQuad);
    }
}