using UnityEngine;

public class Brick : MonoBehaviour
{
    [SerializeField] GameObject skinTf;
    bool isCollect = false;
    public bool IsCollect
    {
        get => isCollect;
        set
        {
            isCollect = value;
            skinTf.SetActive(!value);
        }
    }

    void Start()
    {
        IsCollect = false;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(CONSTANTS.PLAYER_TAG))
        {
            Player player = other.GetComponent<Player>();
            if (player != null && !IsCollect)
            {
                player.AddBrick();
                IsCollect = true;
            }
        }
    }
}
