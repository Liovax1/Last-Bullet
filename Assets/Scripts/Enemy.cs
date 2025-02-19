using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public int health = 3;
    public Renderer enemyRenderer;
    private Color originalColor;

    private NavMeshAgent agent; // Le NavMeshAgent pour d�placer l'ennemi
    private CharacterController characterController; // Le CharacterController
    public Transform player; // R�f�rence au joueur
    public float moveSpeed = 3f; // Vitesse de d�placement de l'ennemi
    private PlayerController playerController;
    public AudioClip[] monsterSounds;  // R�f�rence au bruit du monstre
    private AudioSource audioSource;  // R�f�rence � l'AudioSource

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        characterController = GetComponent<CharacterController>();
        originalColor = enemyRenderer.material.color;
        playerController = FindObjectOfType<PlayerController>();
        audioSource = GetComponent<AudioSource>();  // R�cup�re l'AudioSource attach� � l'ennemi

        // Trouver le joueur automatiquement
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (agent != null)
        {
            agent.updateRotation = false;
        }
    }


    void Update()
    {
        if (player != null)
        {
            MoveTowardsPlayer();
        }
        // Exemple de jouer un bruit de monstre quand l'ennemi se d�place ou attaque
        if (ShouldPlayMonsterSound())  // Si une condition est remplie (comme l'ennemi attaque)
        {
            PlayMonsterSound();
        }
    }

    bool ShouldPlayMonsterSound()
    {
        // Logique pour savoir si le son doit �tre jou�
        // Par exemple, si l'ennemi attaque le joueur, ou si il fait une animation sp�cifique
        return true;
    }

    void PlayMonsterSound()
    {
        if (audioSource != null && monsterSounds.Length > 0)
        {
            int randomIndex = Random.Range(0, monsterSounds.Length);  // Choisir un son al�atoire
            audioSource.PlayOneShot(monsterSounds[randomIndex]);  // Joue le bruit al�atoire
        }
    }

    void MoveTowardsPlayer()
    {
        if (agent != null)
        {
            // Fait avancer l'ennemi vers le joueur
            agent.SetDestination(player.position);
        }

        // Utilise le CharacterController pour appliquer le mouvement
        if (characterController != null)
        {
            // Si NavMeshAgent est actif, applique le mouvement
            if (agent != null && agent.enabled)
            {
                Vector3 moveDirection = (player.position - transform.position).normalized;
                moveDirection.y = 0; // Garder l'ennemi sur le m�me niveau
                characterController.Move(moveDirection * moveSpeed * Time.deltaTime);
            }
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        StartCoroutine(FlashDamage());

        if (health <= 0)
        {
            Die();
        }
    }

    IEnumerator FlashDamage()
    {
        enemyRenderer.material.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        enemyRenderer.material.color = originalColor;
    }

    void Die()
    {
        // Incr�mente le score du joueur
        if (playerController != null)
        {
            playerController.score += 1;  // Incr�mente le score
        }
        Destroy(gameObject);
    }
}
