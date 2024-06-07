using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoBehaviour
{
    [SerializeField] GameObject InputManager;
    [SerializeField] GameObject PlacementSystem;
    [SerializeField] GameObject PencilSword;
    [SerializeField] GameObject Plane;
    // Start is called before the first frame update
    void Start()
    {
        InputManager.SetActive(false);
        PlacementSystem.SetActive(false);
        PencilSword.SetActive(false);
        Plane.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            InputManager.SetActive(true);
            PlacementSystem.SetActive(true);
            PencilSword.SetActive(true);
            Plane.SetActive(true);
        }
    }
}
