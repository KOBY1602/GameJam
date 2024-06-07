using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class FaceTarget : MonoBehaviour
{
    public Transform target;
   


    // Update is called once per frame
    void Update()
    {
        // Check if target is assigned
        if (target != null)
        {
            // Calculate the direction from the current object to the target
            Vector3 directionToTarget = target.position - transform.position;

            

            // Rotate the object to face the target
            if (directionToTarget != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(directionToTarget);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);
            }
        }
    }
}
