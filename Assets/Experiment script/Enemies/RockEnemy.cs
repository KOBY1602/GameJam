using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class RockEnemy : MonoBehaviour
{
    public Transform player;
    //public GameObject gun;

    //Stats
    public int health;

    //Check for Ground/Obstacles
    public LayerMask whatIsGround, whatIsPlayer;

    //Patroling
    public Vector3 walkPoint;
    public bool walkPointSet;
    public float walkPointRange;
    public float patrolSpeed = 3f;

    //Attack Player
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    //Attack Cooldown
    public float attackCooldown = 2f; // Time in seconds before the enemy can be attacked again
    private bool canBeAttacked = true;

    //States
    public bool isDead;
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    //Special
    public GameObject projectile;
    public float chaseSpeed = 5f;
    public float attackSpeed = 0f;

    private Health enemyHealth;
    private Renderer enemyRenderer;
    private Color originalColor;

    private void Awake()
    {
        player = FindObjectOfType<FirstPersonController>().transform;
        enemyHealth = this.GetComponent<Health>();
        enemyRenderer = GetComponent<Renderer>();
        if (enemyRenderer != null)
        {
            originalColor = enemyRenderer.material.color;
        }
    }

    private void Update()
    {
        if (!isDead)
        {
            // Check if Player in sight range and within line of sight
            playerInSightRange = CheckPlayerInSight();

            // Check if Player in attack range
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

            if (!playerInSightRange && !playerInAttackRange) Patroling();
            if (playerInSightRange && !playerInAttackRange) ChasePlayer();
            if (playerInAttackRange && playerInSightRange) AttackPlayer();
        }
    }

    private bool CheckPlayerInSight()
    {
        if (Physics.CheckSphere(transform.position, sightRange, whatIsPlayer))
        {
            // Create a direction vector from the enemy to the player
            Vector3 directionToPlayer = (player.position - transform.position).normalized;

            // Cast a ray from the enemy to the player
            if (Physics.Raycast(transform.position, directionToPlayer, out RaycastHit hit, sightRange))
            {
                // Check if the ray hit the player
                if (hit.transform == player)
                {
                    return true;
                }
            }
        }
        return false;
    }

    private void Patroling()
    {
        if (isDead) return;
        //UnityEngine.Debug.Log("Patrolling");


        if (!walkPointSet) SearchWalkPoint();

        // Calculate direction and walk to Point
        if (walkPointSet)
        {
            transform.position = Vector3.MoveTowards(transform.position, walkPoint, patrolSpeed * Time.deltaTime);
        }

        // Calculates DistanceToWalkPoint
        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        // Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        // Create a potential walk point
        Vector3 potentialWalkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(potentialWalkPoint, Vector3.down, out RaycastHit hit, 3f, whatIsGround))
        {
            walkPoint = hit.point;
            walkPointSet = true;
        }
    }

    private void ChasePlayer()
    {
        if (isDead) return;
        //UnityEngine.Debug.Log("Chasing");
        transform.position = Vector3.MoveTowards(transform.position, player.position, chaseSpeed * Time.deltaTime);
    }

    private void AttackPlayer()
    {
        if (isDead) return;
        //UnityEngine.Debug.Log("Attacking");
        transform.position = Vector3.MoveTowards(transform.position, transform.position, attackSpeed * Time.deltaTime);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            // Attack
            Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();

            rb.AddForce(transform.forward * 15f, ForceMode.Impulse);
            rb.AddForce(transform.up * 2, ForceMode.Impulse);

            Destroy(rb.gameObject, 3f);

            alreadyAttacked = true;
            Invoke("ResetAttack", timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        if (isDead) return;

        alreadyAttacked = false;
    }

    public void TakeDamage(int damage)
    {
        if (canBeAttacked && !isDead)
        {
            health -= damage;
            FlashFeedback();

            if (health <= 0)
            {
                isDead = true;
                Destroy();
            }

            canBeAttacked = false;
            Invoke("ResetAttackCooldown", attackCooldown);
        }
    }

    private void ResetAttackCooldown()
    {
        canBeAttacked = true;
    }

    public void FlashFeedback()
    {
        StartCoroutine(FlashRed());
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }

    private IEnumerator FlashRed()
    {
        if (enemyRenderer != null)
        {
            //Debug.Log("Changed Red");
            enemyRenderer.material.color = Color.red;
            yield return new WaitForSeconds(0.5f); // Adjust the duration as needed
            enemyRenderer.material.color = originalColor;
        }
    }
}
