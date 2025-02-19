using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    public float speed = 3.5f;
    public float attackRange = 2f;
    public int damage = 1;
    public float attackCooldown = 1f;

    private NavMeshAgent agent;
    private float nextAttackTime = 0f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;

        // Trouver le joueur automatiquement
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }


    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance > attackRange)
        {
            agent.SetDestination(player.position);
        }
        else if (Time.time >= nextAttackTime)
        {
            AttackPlayer();
            nextAttackTime = Time.time + attackCooldown;
        }
    }

    void AttackPlayer()
    {
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage);
        }
    }
}
