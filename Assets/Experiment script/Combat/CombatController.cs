using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;

public class CombatController : MonoBehaviour
{
    [SerializeField] GameObject InputManager;
    [SerializeField] GameObject PlacementSystem;
    [SerializeField] GameObject PencilSword;
    [SerializeField] GameObject Plane;

    [SerializeField] GameObject SwordHandle;

    public FirstPersonController firstPersonController;
    public FaceTarget FaceTarget;
    public GameObject animationParent;
    private Animator animator;
    public float reloadTime;
    public bool isReloading;
    public bool isDefending;

    public SliceManager sliceManager; // Reference to the SliceManager
    public HealthManager healthManager; // Reference to the HealthManager

    public int damageIncreaseAmount = 10; // Amount to increase damage per reload
    public float maxHealthDecreaseAmount = 10f; // Amount to decrease max health per reload


    public GameObject reloadParticlePrefab;
    public Transform reloadLocation;
    // Start is called before the first frame update


    //for mouse 
    private Vector3 lastMousePosition;
    private float horizontalVelocity;

    private int isMovingRight; // 1 = right, 0 = left, 3 = not moving


    void Start()
    {
        
        InputManager.SetActive(false);
        PlacementSystem.SetActive(false);
        Plane.SetActive(false);
        firstPersonController = FindObjectOfType<FirstPersonController>();
        animator = animationParent.GetComponent<Animator>();
        if ( animator != null )
        {
            UnityEngine.Debug.Log("Valid");
        }
        // Initialize last mouse position
        lastMousePosition = Input.mousePosition;
      
    }

    // Update is called once per frame
    void Update()
    {
        //Defending
         if(Input.GetMouseButton(1) && !isReloading)
        {
            isDefending = true;
            firstPersonController.Attacking();
            InputManager.SetActive(true);
            PlacementSystem.SetActive(true);
            PencilSword.SetActive(true);
            Plane.SetActive(true);
            Cursor.lockState = CursorLockMode.Confined;
            FaceTarget.DefendForm();
        }
         //Attacking
        else if (Input.GetMouseButton(0) && !isReloading)
        {
            
            firstPersonController.Attacking();
            InputManager.SetActive(true);
            PlacementSystem.SetActive(true);
            PencilSword.SetActive(true);
            Plane.SetActive(true);
            Cursor.lockState = CursorLockMode.Confined;
            FaceTarget.AttackForm();
            isDefending = false;
        }
        else
        {
            isDefending = false;
            Cursor.lockState = CursorLockMode.Locked;
            firstPersonController.NotAttacking();
            InputManager.SetActive(false);
            PlacementSystem.SetActive(false);
            Plane.SetActive(false);
        }
        if(Input.GetKeyDown(KeyCode.R) && !isReloading)
        {
            //Reload Particles
            GameObject reloadPart = Instantiate(reloadParticlePrefab, reloadLocation.transform.position, Quaternion.identity);
            Destroy(reloadPart, 1f);

            SwordHandle.transform.localPosition = new Vector3((float)7.04, (float)2.83, (float)-14.34);
            Cursor.lockState = CursorLockMode.Locked;
            firstPersonController.NotAttacking();
            InputManager.SetActive(false);
            PlacementSystem.SetActive(false);
            Plane.SetActive(false);
            animator.SetTrigger("Reload");

            // Increase damage and decrease max health
            sliceManager.damageAmount += damageIncreaseAmount;
            healthManager.DecreaseMaxHealth(maxHealthDecreaseAmount);

            StartCoroutine(ReloadTime());                   
        }
        //else if (Input.GetKeyDown(KeyCode.)


        // Calculate horizontal velocity of the mouse
        float currentMouseX = Input.mousePosition.x;
        horizontalVelocity = (currentMouseX - lastMousePosition.x) / Time.deltaTime;

        // Update last mouse position
        lastMousePosition = Input.mousePosition;

        // Determine mouse direction
        if (horizontalVelocity > 0)
        {
            //UnityEngine.Debug.Log("Mouse is moving right");
            isMovingRight = 1;
        }
        else if (horizontalVelocity < 0)
        {
            //UnityEngine.Debug.Log("Mouse is moving left");
            isMovingRight = 0;
        }
        else
        {
            //UnityEngine.Debug.Log("Mouse is not moving horizontally");
            isMovingRight = 3;
        }
    }
    
    private IEnumerator ReloadTime()
    {
        isReloading = true;
        yield return new WaitForSeconds((float)3.4);
        isReloading = false;
       
    }
   
}
