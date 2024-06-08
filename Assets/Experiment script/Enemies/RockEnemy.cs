//using JetBrains.Rider.Unity.Editor;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.AI;

//public class RockEnemy : MonoBehaviour
//{
//    public NavMeshAgent agent;

//    public Transform player;
//    //public GameObject gun;

//    //Stats
//    public int health;

//    //Check for Ground/Obstacles
//    public LayerMask whatIsGround, whatIsPlayer;

//    //Patroling
//    public Vector3 walkPoint;
//    public bool walkPointSet;
//    public float walkPointRange;

//    //Attack Player
//    public float timeBetweenAttacks;
//    bool alreadyAttacked;

//    //States
//    public bool isDead;
//    public float sightRange, attackRange;
//    public bool playerInSightRange, playerInAttackRange;


//    //Special
//    public GameObject projectile;

//    private void Awake()
//    {
//        player = FindObjectOfType<FirstPersonController>().transform;
//        agent = GetComponent<NavMeshAgent>();
//    }
//    private void Update()
//    {
//        if (!isDead)
//        {
//            //Check if Player in sightrange
//            playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);

//            //Check if Player in attackrange
//            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

//            if (!playerInSightRange && !playerInAttackRange) Patroling();
//            if (playerInSightRange && !playerInAttackRange) ChasePlayer();
//            if (playerInAttackRange && playerInSightRange) AttackPlayer();
//        }
//    }

//    private void Patroling()
//    {
//        if (isDead) return;

//        if (!walkPointSet) SearchWalkPoint();

//        //Calculate direction and walk to Point
//        if (walkPointSet)
//        {
//            agent.SetDestination(walkPoint);

//            //Vector3 direction = walkPoint - transform.position;
//            //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 0.15f);
//        }

//        //Calculates DistanceToWalkPoint
//        Vector3 distanceToWalkPoint = transform.position - walkPoint;

//        //Walkpoint reached
//        if (distanceToWalkPoint.magnitude < 1f)
//            walkPointSet = false;
//    }
//    private void SearchWalkPoint()
//    {
//        float randomZ = Random.Range(-walkPointRange, walkPointRange);
//        float randomX = Random.Range(-walkPointRange, walkPointRange);

//        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

//        if (Physics.Raycast(walkPoint, -transform.up, 2, whatIsGround))
//            walkPointSet = true;
//    }
//    private void ChasePlayer()
//    {
//        if (isDead) return;

//        agent.SetDestination(player.position);
//    }
//    private void AttackPlayer()
//    {
//        if (isDead) return;

//        //Make sure enemy doesn't move
//        agent.SetDestination(transform.position);

//        transform.LookAt(player);

//        if (!alreadyAttacked)
//        {

//            //Attack
//            Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();

//            rb.AddForce(transform.forward * 20f, ForceMode.Impulse);
//            rb.AddForce(transform.up * 4, ForceMode.Impulse);

//            alreadyAttacked = true;
//            Invoke("ResetAttack", timeBetweenAttacks);
//        }
//    }
//    private void ResetAttack()
//    {
//        if (isDead) return;

//        alreadyAttacked = false;
//    }
//    public void TakeDamage(int damage)
//    {
//        health -= damage;

//        if (health < 0)
//        {
//            isDead = true;
//            Invoke("Destroyy", 2.8f);
//        }
//    }
//    private void Destroyy()
//    {
//        Destroy(gameObject);
//    }

//    private void OnDrawGizmosSelected()
//    {
//        Gizmos.color = Color.red;
//        Gizmos.DrawWireSphere(transform.position, attackRange);
//        Gizmos.color = Color.yellow;
//        Gizmos.DrawWireSphere(transform.position, sightRange);
//    }
//}


//using JetBrains.Rider.Unity.Editor;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.AI;

//public class RockEnemy : MonoBehaviour
//{
//    public NavMeshAgent agent;

//    public Transform player;
//    //public GameObject gun;

//    //Stats
//    public int health;

//    //Check for Ground/Obstacles
//    public LayerMask whatIsGround, whatIsPlayer;

//    //Patroling
//    public Vector3 walkPoint;
//    public bool walkPointSet;
//    public float walkPointRange;

//    //Attack Player
//    public float timeBetweenAttacks;
//    bool alreadyAttacked;

//    //States
//    public bool isDead;
//    public float sightRange, attackRange;
//    public bool playerInSightRange, playerInAttackRange;

//    //Special
//    public GameObject projectile;

//    private void Awake()
//    {
//        player = FindObjectOfType<FirstPersonController>().transform;
//        agent = GetComponent<NavMeshAgent>();
//    }

//    private void Update()
//    {
//        if (!isDead)
//        {
//            // Check if Player in sight range and within line of sight
//            playerInSightRange = CheckPlayerInSight();

//            // Check if Player in attack range
//            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

//            if (!playerInSightRange && !playerInAttackRange) Patroling();
//            if (playerInSightRange && !playerInAttackRange) ChasePlayer();
//            if (playerInAttackRange && playerInSightRange) AttackPlayer();
//        }
//    }

//    private bool CheckPlayerInSight()
//    {
//        if (Physics.CheckSphere(transform.position, sightRange, whatIsPlayer))
//        {
//            // Create a direction vector from the enemy to the player
//            Vector3 directionToPlayer = (player.position - transform.position).normalized;

//            // Cast a ray from the enemy to the player
//            if (Physics.Raycast(transform.position, directionToPlayer, out RaycastHit hit, sightRange, whatIsPlayer))
//            {
//                // Check if the ray hit the player
//                if (hit.transform == player)
//                {
//                    Debug.Log("Player in sight!");
//                    return true;
//                }
//            }
//        }
//        return false;
//    }

//    private void Patroling()
//    {
//        if (isDead) return;

//        if (!walkPointSet) SearchWalkPoint();

//        //Calculate direction and walk to Point
//        if (walkPointSet)
//        {
//            Debug.Log("Walking to point: " + walkPoint);
//            agent.SetDestination(walkPoint);
//        }

//        //Calculates DistanceToWalkPoint
//        Vector3 distanceToWalkPoint = transform.position - walkPoint;

//        //Walkpoint reached
//        if (distanceToWalkPoint.magnitude < 1f)
//            walkPointSet = false;
//    }

//    private void SearchWalkPoint()
//    {
//        Debug.Log("Searching for walk point");
//        float randomZ = Random.Range(-walkPointRange, walkPointRange);
//        float randomX = Random.Range(-walkPointRange, walkPointRange);

//        // Create a potential walk point
//        Vector3 potentialWalkPoint = new Vector3(transform.position.x + randomX, transform.position.y + 2f, transform.position.z + randomZ);

//        if (Physics.Raycast(potentialWalkPoint, Vector3.down, out RaycastHit hit, 3f, whatIsGround))
//        {
//            walkPoint = hit.point;
//            walkPointSet = true;
//            Debug.Log("Walk point set: " + walkPoint);
//        }
//    }

//    private void ChasePlayer()
//    {
//        if (isDead) return;

//        Debug.Log("Chasing player");
//        agent.SetDestination(player.position);
//    }

//    private void AttackPlayer()
//    {
//        if (isDead) return;

//        //Make sure enemy doesn't move
//        agent.SetDestination(transform.position);

//        transform.LookAt(player);

//        if (!alreadyAttacked)
//        {
//            Debug.Log("Attacking player");

//            //Attack
//            Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();

//            rb.AddForce(transform.forward * 20f, ForceMode.Impulse);
//            rb.AddForce(transform.up * 4, ForceMode.Impulse);

//            alreadyAttacked = true;
//            Invoke("ResetAttack", timeBetweenAttacks);
//        }
//    }

//    private void ResetAttack()
//    {
//        if (isDead) return;

//        alreadyAttacked = false;
//    }

//    public void TakeDamage(int damage)
//    {
//        health -= damage;

//        if (health <= 0)
//        {
//            isDead = true;
//            Invoke("Destroyy", 2.8f);
//        }
//    }

//    private void Destroyy()
//    {
//        Destroy(gameObject);
//    }

//    private void OnDrawGizmosSelected()
//    {
//        Gizmos.color = Color.red;
//        Gizmos.DrawWireSphere(transform.position, attackRange);
//        Gizmos.color = Color.yellow;
//        Gizmos.DrawWireSphere(transform.position, sightRange);
//    }
//}

using JetBrains.Rider.Unity.Editor;
using System.Collections;
using System.Collections.Generic;
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

    //States
    public bool isDead;
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    //Special
    public GameObject projectile;
    public float chaseSpeed = 5f;
    public float attackSpeed = 0f;

    private void Awake()
    {
        player = FindObjectOfType<FirstPersonController>().transform;
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

        Debug.Log("Chasing player");
        transform.position = Vector3.MoveTowards(transform.position, player.position, chaseSpeed * Time.deltaTime);
    }

    private void AttackPlayer()
    {
        if (isDead) return;

        transform.position = Vector3.MoveTowards(transform.position, transform.position, attackSpeed * Time.deltaTime);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
        

            // Attack
            Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();

            rb.AddForce(transform.forward * 20f, ForceMode.Impulse);
            rb.AddForce(transform.up * 4, ForceMode.Impulse);

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
        health -= damage;

        if (health <= 0)
        {
            isDead = true;
            Invoke("Destroy", 2.8f);
        }
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
}
