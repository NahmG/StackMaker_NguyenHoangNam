using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIButton : MonoBehaviour
{
    public Action _OnClick;

    public enum STATE
    {
        DISABLE = 0,
        ACTIVE = 1
    }

    [SerializeField] Button button;
    [SerializeField] TMP_Text textButton;
    [SerializeField] Image imageButton;

    [SerializeField] Sprite[] sprites;
    [SerializeField] string[] text;

    STATE state;

    void Awake()
    {
        button.onClick.AddListener(() => _OnClick?.Invoke());
    }

    void OnDestroy()
    {
        button.onClick.RemoveListener(() => _OnClick?.Invoke());
    }

    public void SetData(string text)
    {
        if (textButton != null)
            textButton.text = text;
    }

    public void SetState(STATE state)
    {
        this.state = state;

        if (imageButton != null && sprites.Length > (int)state)
        {
            imageButton.sprite = sprites[(int)state];
        }
        if (text.Length > (int)state)
        {
            SetData(text[(int)state]);
        }
    }

    public void SetInteractive(bool state)
    {
        button.interactable = state;
    }
}