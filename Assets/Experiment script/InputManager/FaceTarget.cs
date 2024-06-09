using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class FaceTarget : MonoBehaviour
{
    public Transform attackTarget;
    public Transform defendTarget;
    public Transform reloadTarget;
    public Vector3 idleRotation;
    public Vector3 idlePosition;
    public Transform reloadPos;
    public CombatController controller;
    public Transform startPos;
    // Update is called once per frame
    private void Start()
    {
        
    }
    void Update()
    {
        if(controller.isReloading == true)
        {
            ReloadingForm();
            this.transform.position = reloadPos.position;
        }
        else
        {
            this.transform.position = startPos.position;
        }
    }
    public void IdleForm()
    {
        this.transform.position = idlePosition;
        this.transform.rotation = Quaternion.Euler(idleRotation);
    }
    public void ReloadingForm()
    {
        if (reloadTarget != null)
        {
            //Calculate the direction from the current object to the target
            Vector3 directionToTarget = reloadTarget.position - transform.position;

            //Rotate the object to face the target
            if (directionToTarget != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(directionToTarget, Vector3.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);
            }

        }
    }
    public void AttackForm()
    {
        // Check if target is assigned
        if (attackTarget != null)
        {
            //Calculate the direction from the current object to the target
            Vector3 directionToTarget = attackTarget.position - transform.position;

            //Rotate the object to face the target
            if (directionToTarget != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(directionToTarget, Vector3.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);
            }

        }
    }
    public void DefendForm()
    {
        if (defendTarget != null)
        {
            // Calculate the direction from the current object to the target
            Vector3 directionToTarget = defendTarget.position - transform.position;
            

            // Rotate the object to face the target
            if (directionToTarget != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(directionToTarget, Vector3.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);
            }
        }
    }
}
