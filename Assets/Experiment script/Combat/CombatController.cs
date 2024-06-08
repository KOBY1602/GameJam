using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class CombatController : MonoBehaviour
{
    [SerializeField] GameObject InputManager;
    [SerializeField] GameObject PlacementSystem;
    [SerializeField] GameObject PencilSword;
    [SerializeField] GameObject Plane;

    [SerializeField] GameObject SwordParent;

    public FirstPersonController firstPersonController;
    public FaceTarget FaceTarget;
    public Animation AnimationParent;


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

        AnimationParent = gameObject.GetComponent<Animation>();
        // Initialize last mouse position
        lastMousePosition = Input.mousePosition;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButton(0))
        {
            
            firstPersonController.Attacking();
            InputManager.SetActive(true);
            PlacementSystem.SetActive(true);
            PencilSword.SetActive(true);
            Plane.SetActive(true);
            Cursor.lockState = CursorLockMode.Confined;
            FaceTarget.AttackForm();
        }
        else
        {

            Cursor.lockState = CursorLockMode.Locked;
            firstPersonController.NotAttacking();
            InputManager.SetActive(false);
            PlacementSystem.SetActive(false);
            Plane.SetActive(false);
        }
        if(Input.GetKeyDown(KeyCode.R))
        {
            Cursor.lockState = CursorLockMode.Locked;
            firstPersonController.NotAttacking();
            InputManager.SetActive(false);
            PlacementSystem.SetActive(false);
            Plane.SetActive(false);
            AnimationParent.Play("Reload1");

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
   
}
