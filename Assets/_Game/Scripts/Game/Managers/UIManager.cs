using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    public static UIManager Ins;
    [SerializeField] Transform tf_parent;
    UICanvas[] canvas;
    public Dictionary<UI, UICanvas> UIPrefabs = new();
    public Dictionary<UI, UICanvas> UICanvas = new();

    void Awake()
    {
        Ins = this;
    }

    public void OpenUI(UI id, object param = null)
    {
        GetUI(id).Open(param);
    }

    public void CloseUI(UI id)
    {
        GetUI(id).Close();
    }

    public bool IsOpen(UI id)
    {
        return IsLoad(id) && GetUI(id).gameObject.activeInHierarchy;
    }

    public bool IsLoad(UI id)
    {
        return UICanvas.ContainsKey(id) && UICanvas[id] != null;
    }

    public UICanvas GetUI(UI id)
    {
        if (!IsLoad(id))
        {
            UICanvas canvas = Instantiate(GetUIPrefab(id), tf_parent);
            UICanvas[id] = canvas;
        }
        return UICanvas[id];
    }

    public void CloseAll()
    {
        foreach (var key in UICanvas.Keys)
        {
            if (IsOpen(key))
                CloseUI(key);
        }
    }

    public void PreloadUI(UI id)
    {
        if (!IsLoad(id))
        {
            UICanvas canvas = Instantiate(GetUIPrefab(id), tf_parent);
            canvas.gameObject.SetActive(false);
            UICanvas[id] = canvas;
        }
    }

    public UICanvas GetUIPrefab(UI id)
    {
        if (UIPrefabs.ContainsKey(id) && UIPrefabs[id] != null)
            return UIPrefabs[id];

        canvas ??= Resources.LoadAll<UICanvas>("UI");
        for (int i = 0; i < canvas.Length; i++)
        {
            if (canvas[i].ID == id)
            {
                UIPrefabs[id] = canvas[i];
                break;
            }
        }
        return UIPrefabs[id];
    }
}

public enum UI
{
    MAIN_MENU,
    GAME_PLAY,
    WIN,
    SETTING,
    LOADING
}