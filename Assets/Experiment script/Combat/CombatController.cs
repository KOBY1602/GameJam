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
    // Start is called before the first frame update
    void Start()
    {
        InputManager.SetActive(false);
        PlacementSystem.SetActive(false);
        Plane.SetActive(false);
        firstPersonController = FindObjectOfType<FirstPersonController>();
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
        //else if (Input.GetKeyDown(KeyCode.)
    }
}
