using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using UnityEngine;

public class FaceTarget : MonoBehaviour
{
    public Transform target;

    public Vector3 idleRotation;
    public Vector3 idlePosition;

    // Update is called once per frame
    void Update()
    {

    }
    public void IdleForm()
    {
        this.transform.position = idlePosition;
        this.transform.rotation = Quaternion.Euler(idleRotation);
    }
    public void AttackForm()
    {
        // Check if target is assigned
        if (target != null)
        {
            // Calculate the direction from the current object to the target
            Vector3 directionToTarget = target.position - transform.position;

            // Rotate the object to face the target
            if (directionToTarget != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(directionToTarget, Vector3.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);
            }
        }
    }
}
