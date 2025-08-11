using System;
using DG.Tweening;
using UnityEngine;

public class LoadingCanvas : UICanvas
{
    public override UI ID => UI.LOADING;

    [SerializeField] RectTransform mask;
    [SerializeField] float loadTime;
    Action action;
    Sequence seq;

    public override void Open(object param = null)
    {
        base.Open(param);

        if (param is Action)
        {
            action = param as Action;
        }

        AnimLoading();
    }

    void Update()
    {
        startTime += Time.deltaTime;

        if (startTime >= loadTime)
        {
            action();
            startTime = 0;
        }
    }

    void AnimLoading()
    {
        if (seq != null && seq.IsActive())
        {
            seq.Kill();
        }

        seq = DOTween.Sequence();
        seq.Append(mask.DOScale(0, loadTime));

        seq.AppendInterval(.5f);

        seq.Append(mask.DOScale(1, loadTime))
           .OnComplete(() => Close());

        seq.SetAutoKill(true);
    }
}