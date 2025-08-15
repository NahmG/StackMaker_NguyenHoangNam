using TMPro;
using UnityEngine;

public class TextItem : WheelItemBase
{
    [SerializeField]
    string text;

    public override void UpdateDisplay()
    {
        if (textItem != null)
            textItem.text = text;
    }
}