using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public Rigidbody rb;
    public Transform player;
    public float knockbackForce;
    // Start is called before the first frame update
    void Start()
    {
        rb.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void KnockBack()
    {
        // Calculate the direction from the enemy to the player
        Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;

        // Apply knockback force in the opposite direction
        Rigidbody enemyRigidbody = GetComponent<Rigidbody>();
        enemyRigidbody.AddForce(-directionToPlayer * knockbackForce, ForceMode.Impulse);
    }
    void OnCollisionEnter(Collision collision)
    {
        // Check if the colliding object has the target tag
        if (collision.gameObject.CompareTag("Sword"))
        {
            UnityEngine.Debug.Log("Yes");

            KnockBack();
        }
    }
}
