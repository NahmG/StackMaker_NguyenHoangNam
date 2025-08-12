using UnityEngine;

public class Diamond : MonoBehaviour
{
    [SerializeField] int value;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(CONSTANTS.PLAYER_TAG))
        {
            //Add diamond
            GameplayManager.Ins.AddDiamond(value);
            gameObject.SetActive(false);
        }
    }
}
