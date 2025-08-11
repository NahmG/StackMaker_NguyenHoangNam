using UnityEngine;

public class Push : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(CONSTANTS.PLAYER_TAG))
        {
            Player player = other.GetComponent<Player>();
            if (player)
            {
                player.isPushBlock = true;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(CONSTANTS.PLAYER_TAG))
        {
            Player player = other.GetComponent<Player>();
            if (player)
            {
                player.isPushBlock = false;
            }
        }
    }
}