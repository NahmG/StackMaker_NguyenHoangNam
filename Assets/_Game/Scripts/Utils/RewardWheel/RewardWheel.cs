using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SpinWheel : MonoBehaviour
{
    public Action<int> _OnSpinComplete;

    [Header("Setting")]
    public RectTransform content;
    public float cellWidth;
    public float spacing;
    public int loopCount;
    public float spinSpeed;

    [Header("Parameter")]
    [SerializeField] WheelItemBase[] itemData;
    float contentWidth;

    public int TotalItem => itemData.Length;

    void Awake()
    {
        Initialize();
    }

    public void Initialize()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < itemData.Length; j++)
            {
                WheelItemBase item = Instantiate(itemData[j], content);
                item.UpdateDisplay();
            }
        }

        // Total cells
        int totalCells = itemData.Length;
        contentWidth = totalCells * cellWidth + (totalCells - 1) * spacing;

        Vector2 pos = content.anchoredPosition;
        pos = new(pos.x - contentWidth - spacing, pos.y);

        content.anchoredPosition = pos;
    }

    public void SpinTo(int targetIndex)
    {
        float currentX = content.anchoredPosition.x;
        float targetX = currentX - (targetIndex * (cellWidth + spacing));

        // Do the spin
        Sequence seq = DOTween.Sequence();

        Vector2 startPos = content.anchoredPosition;
        float totalDistance = currentX - contentWidth;

        content.DOAnchorPosX(totalDistance, spinSpeed)
            .SetEase(Ease.Linear)
            .SetSpeedBased(true)
            .SetLoops(loopCount, LoopType.Restart)
            .OnComplete(() =>
                {
                    content.anchoredPosition = startPos;

                    content.DOAnchorPosX(targetX, spinSpeed)
                    .SetEase(Ease.OutQuart)
                    .SetSpeedBased(true).OnComplete(() =>
                    {
                        _OnSpinComplete?.Invoke(targetIndex);
                    });
                }
            );
    }

    public T GetResult<T>(int targetIndex) where T : WheelItemBase
    {
        return itemData[targetIndex] as T;
    }
}