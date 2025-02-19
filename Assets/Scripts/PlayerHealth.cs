using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 10;
    public int currentHealth;
    public Image damageOverlay; // Image rouge pour feedback d�g�ts
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
        // R�initialiser la position du joueur, ou faire respawn
        transform.position = new Vector3((float)-193.31, (float)10.66, 0);  // Exemple : remettre le joueur � une position sp�cifique
        currentHealth = maxHealth;  // R�initialiser la sant�
    }

    void Die()
    {
        Debug.Log("Player is Dead!");
        // G�rer le game over ici
    }

    IEnumerator ShowDamageEffect()
    {
        damageOverlay.color = new Color(1, 0, 0, 0.5f);
        yield return new WaitForSeconds(overlayDuration);
        damageOverlay.color = new Color(1, 0, 0, 0);
    }
}
