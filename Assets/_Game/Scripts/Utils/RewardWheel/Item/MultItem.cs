using TMPro;
using UnityEngine;

public class MultItem : WheelItemBase
{
    [SerializeField] float mul;
    public float Mul => mul;

    public override void UpdateDisplay()
    {
        if (textItem != null)
            textItem.text = $"x{mul}";
    }
}