using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

using URandom = UnityEngine.Random;

public class GetMoreGemPopup : UICanvas
{
    public override UI ID => UI.GET_MORE_GEM;

    [SerializeField] SpinWheel spinWheel;
    [SerializeField] UIButton getMoreBtn;
    [SerializeField] TMP_Text gemText;

    int _currentGem = 150;
    Action _spinAction;

    void Awake()
    {
        getMoreBtn._OnClick += OnGetMoreBtnClick;
        getMoreBtn.SetState(UIButton.STATE.ACTIVE);
        getMoreBtn.SetInteractive(true);

        spinWheel._OnSpinComplete += OnSpinWheelComplete;

        _spinAction = () =>
        {
            GameplayManager.Ins.AddGem(_currentGem);
        };
    }

    void OnDestroy()
    {
        getMoreBtn._OnClick -= OnGetMoreBtnClick;
        spinWheel._OnSpinComplete -= OnSpinWheelComplete;
    }

    public override void Setup()
    {
        base.Setup();
        getMoreBtn.SetState(UIButton.STATE.ACTIVE);
        getMoreBtn.SetInteractive(true);

        gemText.text = $"+{_currentGem}";
    }

    public override void Open(object param = null)
    {
        base.Open(param);

        if (param is int value)
        {
            _currentGem = value;
        }

        GameplayManager.Ins.Player.StopMove();
    }

    public override void Close(float time = 0.2F)
    {
        base.Close(time);

        GameplayManager.Ins.Player.Continue();
        UIManager.Ins.OpenUI(UI.GAME_PLAY, _spinAction);
    }

    void OnGetMoreBtnClick()
    {
        int rnd = URandom.Range(1, spinWheel.TotalItem);
        spinWheel.SpinTo(rnd);

        getMoreBtn.SetState(UIButton.STATE.DISABLE);
        getMoreBtn.SetInteractive(false);
    }

    void OnSpinWheelComplete(int targetIndex)
    {
        float mul = spinWheel.GetResult<MultItem>(targetIndex).Mul;

        int totalGem = (int)(_currentGem * mul);

        //update gemText
        DOVirtual.Int(_currentGem, totalGem, .5f, value =>
        {
            _currentGem = value;
            gemText.text = $"+{value}";
        }).OnComplete(() =>
        {
            DOVirtual.DelayedCall(.5f, () => Close());
        });
    }
}