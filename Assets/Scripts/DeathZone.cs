using UnityEngine;

public class DeathZone : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        // V�rifie si l'objet qui entre dans la zone est le joueur
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                // Appelle une m�thode pour tuer le joueur ou le r�initialiser
                playerHealth.ResetPlayer();  // Ou si tu veux r�initialiser : playerHealth.ResetPlayer();
            }
        }
    }
}
