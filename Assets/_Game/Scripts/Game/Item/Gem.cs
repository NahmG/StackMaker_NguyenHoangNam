using UnityEngine;

public class Gem : MonoBehaviour
{
    [SerializeField] int value;
    [Tooltip("Bonus rate in %"), Range(0, 100), SerializeField] int bonusRate;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(CONSTANTS.PLAYER_TAG))
        {
            float rnd = Random.value;
            if (rnd <= bonusRate / 100)
            {
                UIManager.Ins.CloseAll();
                UIManager.Ins.OpenUI(UI.GET_MORE_GEM, value);
            }
            else
            {
                GameplayManager.Ins.AddGem(value);
            }

            gameObject.SetActive(false);
        }
    }
}
