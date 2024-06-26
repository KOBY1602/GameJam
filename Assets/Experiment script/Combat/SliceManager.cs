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
    public GameObject deathParticlePrefab;

    public Camera mainCamera; // Reference to the main camera

    public AudioSource audioSource; // Reference to the AudioSource component
    public AudioClip hitSound; // Reference to the sound clip to play when an enemy is hit


    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main; // Assign main camera if not set
        }

        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>(); // Assign AudioSource if not set
        }
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

    public void Slice()
    {
        Collider[] hits = Physics.OverlapBox(cutPlane.position, new Vector3(5, 0.1f, 5), cutPlane.rotation, layerMask);

        if (hits.Length <= 0)
            return;

        for (int i = 0; i < hits.Length; i++)
        {
            Health enemyHealth = hits[i].gameObject.GetComponent<Health>(); // Get the Health component of the enemy
            RockEnemy rockEnemy = hits[i].gameObject.GetComponent<RockEnemy>(); // Get the RockEnemy component

            if (enemyHealth != null && enemyHealth.currentHealth <= 0)
            {
                // Skip slicing if the enemy is already dead
                continue;
            }

            GameObject hitEffect = Instantiate(hitEffectPrefab, hits[i].transform.position, Quaternion.identity);
            Destroy(hitEffect, 0.5f);

            // Play hit sound
            audioSource.PlayOneShot(hitSound);

            audioSource.PlayOneShot(hitSound);

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


                    GameObject deathPart = Instantiate(deathParticlePrefab, hits[i].transform.position, Quaternion.identity);
                    Destroy(deathPart, 1f);
                    Destroy(hits[i].gameObject);
                }
            }
            else
            {
                // If enemy's health is above the damage threshold, just apply damage
                enemyHealth.TakeDamage(damageAmount);
                rockEnemy.FlashFeedback();
            }

            ShakeCamera();
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

    public void ShakeCamera()
    {
        // Shake the camera using DOTween's DOShakePosition
        mainCamera.transform.DOShakePosition(0.3f, 0.5f, 20, 90, false, true);
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
