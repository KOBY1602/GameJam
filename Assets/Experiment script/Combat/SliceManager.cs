using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;
using DG.Tweening;

public class SliceManager : MonoBehaviour
{
    [SerializeField] private Transform cutPlane;
    public Material crossMaterial;
    public LayerMask layerMask;

    public int damageAmount = 20;
    public int damageThreshold = 20;

    public float attackCooldown = 0.5f; // Cooldown period after each attack
    private bool canAttack = true; // Flag to determine if the player can attack

    public GameObject hitEffectPrefab; // Reference to the hit effect prefab


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (canAttack)
        {
            Slicing();
        }
    }

    public SlicedHull SliceObject(GameObject obj, Material crossSectionMaterial = null)
    {
        // slice the provided object using the transforms of this object
        if (obj.GetComponent<MeshFilter>() == null)
            return null;

        return obj.Slice(cutPlane.position, cutPlane.up, crossSectionMaterial);
    }

    public void Slicing()
    {
        if (Input.GetMouseButton(0))
        {
            cutPlane.GetChild(0).DOComplete();
            cutPlane.GetChild(0).DOLocalMoveX(cutPlane.GetChild(0).localPosition.x * -1, .05f).SetEase(Ease.OutExpo);
            //ShakeCamera();
            Slice();
        }
    }

    //public void Slice()
    //{
    //    Collider[] hits = Physics.OverlapBox(cutPlane.position, new Vector3(5, 0.1f, 5), cutPlane.rotation, layerMask);

    //    if (hits.Length <= 0)
    //        return;

    //    for (int i = 0; i < hits.Length; i++)
    //    {
    //        SlicedHull hull = SliceObject(hits[i].gameObject, crossMaterial);
    //        if (hull != null)
    //        {
    //            GameObject bottom = hull.CreateLowerHull(hits[i].gameObject, crossMaterial);
    //            GameObject top = hull.CreateUpperHull(hits[i].gameObject, crossMaterial);
    //            AddHullComponents(bottom);
    //            AddHullComponents(top);
    //            Destroy(hits[i].gameObject);
    //        }
    //    }
    //}

    public void Slice()
    {
        Collider[] hits = Physics.OverlapBox(cutPlane.position, new Vector3(5, 0.1f, 5), cutPlane.rotation, layerMask);

        if (hits.Length <= 0)
            return;

        for (int i = 0; i < hits.Length; i++)
        {
            Health enemyHealth = hits[i].gameObject.GetComponent<Health>(); // Get the Health component of the enemy

            if (enemyHealth != null && enemyHealth.currentHealth <= 0)
            {
                // Skip slicing if the enemy is already dead
                continue;
            }
            
            GameObject hitEffect = Instantiate(hitEffectPrefab, hits[i].transform.position, Quaternion.identity);
            Destroy(hitEffect,0.5f);

            if (enemyHealth != null && enemyHealth.currentHealth <= damageThreshold)
            {
                // If enemy's health is below or equal to the damage threshold, slice it
                SlicedHull hull = SliceObject(hits[i].gameObject, crossMaterial);
                if (hull != null)
                {
                    GameObject bottom = hull.CreateLowerHull(hits[i].gameObject, crossMaterial);
                    GameObject top = hull.CreateUpperHull(hits[i].gameObject, crossMaterial);
                    AddHullComponents(bottom);
                    AddHullComponents(top);
                    Destroy(hits[i].gameObject);
                }
            }
            else
            {
                // If enemy's health is above the damage threshold, just apply damage
                enemyHealth.TakeDamage(damageAmount);
            }
        }

        // Start the cooldown timer
        StartCoroutine(AttackCooldown());
    }

    IEnumerator AttackCooldown()
    {
        canAttack = false; // Disable attacking
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true; // Enable attacking after the cooldown period
    }


    public void AddHullComponents(GameObject go)
    {
        go.layer = 9;
        Rigidbody rb = go.AddComponent<Rigidbody>();
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        MeshCollider collider = go.AddComponent<MeshCollider>();
        collider.convex = true;

        rb.AddExplosionForce(10, go.transform.position, 20);
    }
}
