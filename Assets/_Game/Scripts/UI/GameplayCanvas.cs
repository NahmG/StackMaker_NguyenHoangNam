using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameplayCanvas : UICanvas
{
    public override UI ID => UI.GAME_PLAY;

    [SerializeField] TMP_Text levelText;
    [SerializeField] Button settingButton;

    [Header("Gem Anim")]
    [SerializeField] Transform gemTextSpawnTf;
    [SerializeField] GameObject gemAddText;
    [SerializeField] Transform gemSpawnTf;
    [SerializeField] GameObject UIGemPref;
    [SerializeField] TMP_Text gemCounter;
    int currentGemCount;


    void Awake()
    {
        settingButton.onClick.AddListener(OnSettingBtnClick);
        UIManager.Ins.PreloadUI(UI.SETTING);

        GameplayManager.Ins.OnUpdateGemCount += OnUpdateGemCount;
    }

    void Update()
    {
        levelText.text = LevelManager.Ins.Level.ToString();
    }

    public void OnSettingBtnClick()
    {
        UIManager.Ins.OpenUI(UI.SETTING);
    }

    public void OnUpdateGemCount()
    {
        AnimateGems();
        ShowGemAddText();
    }

    void AnimateGems()
    {
        int spawnCount = 6;
        float radius = 200f;
        for (int i = 0; i < spawnCount; i++)
        {
            //Spawn gem
            Transform gem = Instantiate(UIGemPref, gemSpawnTf).transform;

            //move to random pos    
            Vector2 randomPos = (Vector2)gemSpawnTf.position + Random.insideUnitCircle * radius;

            Sequence seq = DOTween.Sequence();

            seq.Append(gem.DOMove(randomPos, .2f)
            .SetEase(Ease.OutQuad));

            //fly toward gemCounter
            seq.Append(gem.DOMove(gemCounter.transform.position, .5f)
            .SetDelay(1f)
            .SetEase(Ease.InQuad)
            .OnComplete(() =>
            {
                Destroy(gem.gameObject);
                UpdateGemCounter();
            }));

            seq.SetAutoKill(true);
        }
    }

    void ShowGemAddText()
    {
        Transform gemText = Instantiate(gemAddText, gemTextSpawnTf).transform;
        gemText.localScale = Vector3.zero;
        Sequence seq = DOTween.Sequence();

        seq.Append(gemText.DOScale(1, .2f)
        .SetEase(Ease.OutBack));

        seq.Append(gemText.DOScale(0, .2f)
        .SetDelay(.5f)
        .SetEase(Ease.InBack)
        .OnComplete(() =>
        {
            Destroy(gemText.gameObject);
        }));

        seq.SetAutoKill(true);
    }

    void UpdateGemCounter()
    {
        DOVirtual.Int(currentGemCount, GameplayManager.Ins.Gem, .1f, value =>
        {
            currentGemCount = value;
            gemCounter.text = $"{value}";
        });
    }
}