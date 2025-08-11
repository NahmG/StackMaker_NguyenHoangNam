using System;
using DG.Tweening;
using UnityEngine;

public class LoadingCanvas : UICanvas
{
    public override UI ID => UI.LOADING;

    [SerializeField] RectTransform mask;
    [SerializeField] float loadTime;
    Action action;

    public override void Open(object param = null)
    {
        base.Open(param);

        if (param is Action)
        {
            action = param as Action;
        }

        AnimLoading();
    }

    public override void Setup()
    {
        base.Setup();
        action = null;
    }

    void AnimLoading()
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(mask.DOScale(0, loadTime).OnComplete(() => action()));
        seq.AppendInterval(.5f);
        seq.Append(mask.DOScale(1, loadTime))
           .OnComplete(() => Close());

        seq.SetAutoKill(true);
    }
}