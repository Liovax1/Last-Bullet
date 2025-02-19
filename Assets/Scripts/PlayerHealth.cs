using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 10;
    public int currentHealth;
    public Image damageOverlay; // Image rouge pour feedback dégâts
    public float overlayDuration = 0.2f;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (damageOverlay != null)
        {
            StartCoroutine(ShowDamageEffect());
        }

        if (currentHealth <= 0)
        {
            Die();
        }

        CameraShake cameraShake = Camera.main.GetComponent<CameraShake>();
        if (cameraShake != null)
        {
            StartCoroutine(cameraShake.Shake(0.15f, 0.2f));
        }

    }

    public void ResetPlayer()
    {
        // Réinitialiser la position du joueur, ou faire respawn
        transform.position = new Vector3((float)-193.31, (float)10.66, 0);  // Exemple : remettre le joueur à une position spécifique
        currentHealth = maxHealth;  // Réinitialiser la santé
    }

    void Die()
    {
        Debug.Log("Player is Dead!");
        // Gérer le game over ici
    }

    IEnumerator ShowDamageEffect()
    {
        damageOverlay.color = new Color(1, 0, 0, 0.5f);
        yield return new WaitForSeconds(overlayDuration);
        damageOverlay.color = new Color(1, 0, 0, 0);
    }
}
