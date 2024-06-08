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
    
    public FirstPersonController firstPersonController;
    // Start is called before the first frame update
    void Start()
    {
        InputManager.SetActive(false);
        PlacementSystem.SetActive(false);
        PencilSword.SetActive(false);

        Plane.SetActive(false);
        firstPersonController = FindObjectOfType<FirstPersonController>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            firstPersonController.Attacking();
            InputManager.SetActive(true);
            PlacementSystem.SetActive(true);
            PencilSword.SetActive(true);
            Plane.SetActive(true);

        }
        //else if (Input.GetKeyDown(KeyCode.)
    }
}
