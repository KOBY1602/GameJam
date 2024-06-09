using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocking : MonoBehaviour
{
    
    
    public GameObject hitEffectPrefab;
    public GameObject reloadLocation;
    public float damageAmount;
    public SliceManager sliceManager;


    // Start is called before the first frame update
    void Start()
    {
        reloadLocation = GameObject.Find("ReloadLocation");
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
        if (collision.collider.gameObject.CompareTag("Sword") && GameObject.Find("PlayerCamera").GetComponent<CombatController>().isDefending)
        {
            
            GameObject hitEffect = Instantiate(hitEffectPrefab, reloadLocation.transform.position , Quaternion.identity);
             //destroy the projectile on collision
           Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            GameObject.Find("NewFPSController").GetComponent<HealthManager>().TakeDamage(damageAmount);
            //sliceManager.ShakeCamera();
        }
    }
}
