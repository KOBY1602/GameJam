using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocking : MonoBehaviour
{
    
    
    //public GameObject hitEffectPrefab;

    // Start is called before the first frame update
    void Start()
    {
         GameObject Controller = GameObject.Find("PlayerCamera");
         CombatController combatController = Controller.GetComponent<CombatController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        // Check if the collision is with a target object
        //if (collision.gameObject.CompareTag("Projectile") && GetComponent<CombatController>().isDefending)
        {
           // Debug.Log("Hit");
            //GameObject hitEffect = Instantiate(hitEffectPrefab, collision.transform.position, Quaternion.identity);
            // destroy the projectile on collision
           // Destroy(collision.gameObject);
        }
    }
}
