using UnityEngine;

public class Finish : MonoBehaviour
{
    public GameObject chestOpen;
    public GameObject chestClose;
    public ParticleSystem particle1;
    public ParticleSystem particle2;
    public Animator People;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(CONSTANTS.PLAYER_TAG))
        {
            Player player = other.GetComponent<Player>();
            if (player)
            {
                OnFinish();
                player.OnWin();
            }
        }
    }

    void OnFinish()
    {
        People.gameObject.SetActive(true);
        People.SetTrigger("Finish");

        chestClose.SetActive(false);
        chestOpen.SetActive(true);

        particle1.Play();
        particle2.Play();
    }
}
