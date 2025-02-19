using UnityEngine;

public class DeathZone : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        // Vérifie si l'objet qui entre dans la zone est le joueur
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                // Appelle une méthode pour tuer le joueur ou le réinitialiser
                playerHealth.ResetPlayer();  // Ou si tu veux réinitialiser : playerHealth.ResetPlayer();
            }
        }
    }
}
