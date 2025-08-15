using System;
using TMPro;
using UnityEngine;

public abstract class WheelItemBase : MonoBehaviour
{
    [SerializeField]
    protected TMP_Text textItem;

    public abstract void UpdateDisplay();
}
