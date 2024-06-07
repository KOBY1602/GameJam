using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class PlacementSystem : MonoBehaviour
{
    [SerializeField] private GameObject mouseIndicator;
    [SerializeField] private GetMouse GetMouse;


    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = GetMouse.GetSelectedMapPosition();
        mouseIndicator.transform.position = mousePosition;
    }
}
