using System.Xml.Serialization;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Ins;
    void Awake()
    {
        Ins = this;
    }

    [SerializeField] Level[] levels;

    Level currentLevel;
    int levelIndex;
    public int Level => levelIndex;

    public void OnInit()
    {
        levelIndex = 1;
    }

    public void NextLevel()
    {
        levelIndex = levelIndex < levels.Length ? levelIndex + 1 : levelIndex;
    }

    public Level LoadLevel()
    {
        if (currentLevel != null)
        {
            Destroy(currentLevel.gameObject);
        }
        currentLevel = Instantiate(GetPrefabLevel(), transform);
        return currentLevel;
    }

    Level GetPrefabLevel()
    {
        return levelIndex > 1 ? levels[levelIndex - 1] : levels[0];
    }
}