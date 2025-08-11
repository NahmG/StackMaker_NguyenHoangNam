using UnityEngine;

public class Unbrick : MonoBehaviour
{
    [SerializeField] GameObject skinTf;
    bool isRemove = false;
    public bool IsRemove
    {
        get => isRemove;
        set
        {
            isRemove = value;
            skinTf.SetActive(value);
        }
    }

    void Start()
    {
        IsRemove = false;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(CONSTANTS.PLAYER_TAG))
        {
            Player player = other.GetComponent<Player>();
            if (player != null && !IsRemove)
            {
                player.RemoveBrick();
                IsRemove = true;
            }
        }
    }
}
