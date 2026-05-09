using UnityEngine;

public class ShieldCollision : MonoBehaviour
{


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInvincibility invincibility = other.GetComponent<PlayerInvincibility>();
            if (invincibility != null)
            {
                invincibility.ActivateShield();
                PlayerAudio.Instance.PlayShieldSound();
            }

            Destroy(gameObject);
        }
    }
}