using DG.Tweening;
using UnityEngine;

public abstract class UICanvas : MonoBehaviour
{
    public abstract UI ID { get; }
    protected float startTime;
    [SerializeField] RectTransform Tf;
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] bool hasFadeTransition;

    public virtual void Open(object param = null)
    {
        Setup();
        gameObject.SetActive(true);
        startTime = 0;

        if (hasFadeTransition)
            FadeInTransition();
    }

    public virtual void Setup()
    {
        if (hasFadeTransition)
        {
            Tf.localScale = Vector3.zero;
            canvasGroup.alpha = 0;
        }
    }

    public virtual void Close(float time = .2f)
    {
        if (hasFadeTransition)
            FadeOutTransition();

        DOVirtual.DelayedCall(time, OnClose);
    }

    void OnClose()
    {
        gameObject.SetActive(false);
    }

    void FadeInTransition()
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(canvasGroup.DOFade(1, .2f));
        seq.Join(Tf.DOScale(1, .2f));

        seq.SetEase(Ease.OutQuad);

        seq.SetAutoKill(true);
    }

    void FadeOutTransition()
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(canvasGroup.DOFade(0, .2f));
        seq.Join(Tf.DOScale(0, .2f));

        seq.SetEase(Ease.InQuad);

        seq.SetAutoKill(true);
    }
}