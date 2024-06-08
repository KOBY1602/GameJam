//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using EzySlice;
//using DG.Tweening;

//public class SliceManager : MonoBehaviour
//{
//    [SerializeField] private Transform cutPlane;
//    public Material crossMaterial;
//    public LayerMask layerMask;

//    void Start()
//    {

//    }

//    // Update is called once per frame
//    void Update()
//    {
//        Slicing();
//    }

//    public SlicedHull SliceObject(GameObject obj, Material crossSectionMaterial = null)
//    {
//        // slice the provided object using the transforms of this object
//        if (obj.GetComponent<MeshFilter>() == null)
//            return null;

//        return obj.Slice(cutPlane.position, cutPlane.up, crossSectionMaterial);
//    }

//    public void Slicing()
//    {
//        if (Input.GetMouseButtonDown(0))
//        {
//            cutPlane.GetChild(0).DOComplete();
//            cutPlane.GetChild(0).DOLocalMoveX(cutPlane.GetChild(0).localPosition.x * -1, .05f).SetEase(Ease.OutExpo);
//            //ShakeCamera();
//            Slice();
//        }
//    }

//    public void Slice()
//    {
//        Collider[] hits = Physics.OverlapBox(cutPlane.position, new Vector3(5, 0.1f, 5), cutPlane.rotation, layerMask);

//        if (hits.Length <= 0)
//            return;

//        for (int i = 0; i < hits.Length; i++)
//        {
//            SlicedHull hull = SliceObject(hits[i].gameObject, crossMaterial);
//            if (hull != null)
//            {
//                GameObject bottom = hull.CreateLowerHull(hits[i].gameObject, crossMaterial);
//                GameObject top = hull.CreateUpperHull(hits[i].gameObject, crossMaterial);
//                AddHullComponents(bottom);
//                AddHullComponents(top);
//                Destroy(hits[i].gameObject);
//            }
//        }
//    }

//    public void AddHullComponents(GameObject go)
//    {
//        go.layer = 9;
//        Rigidbody rb = go.AddComponent<Rigidbody>();
//        rb.interpolation = RigidbodyInterpolation.Interpolate;
//        MeshCollider collider = go.AddComponent<MeshCollider>();
//        collider.convex = true;

//        rb.AddExplosionForce(100, go.transform.position, 20);
//    }
//}

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

    private Vector3 lastMousePosition;
    [SerializeField] private float horizontalVelocityThreshold = 1000f; // Adjust as needed

    void Start()
    {
        lastMousePosition = Input.mousePosition;
    }

    // Update is called once per frame
    void Update()
    {

        Debug.Log(IsMouseMovingFast());
        // Check if the left mouse button is held down and the mouse is moving fast enough
        if (Input.GetMouseButtonDown(0) && IsMouseMovingFast())
        {
            Debug.Log("Cut");
            cutPlane.GetChild(0).DOComplete();
            cutPlane.GetChild(0).DOLocalMoveX(cutPlane.GetChild(0).localPosition.x * -1, .05f).SetEase(Ease.OutExpo);
            Slice();
        }
    }

    // Check if the mouse is moving fast enough horizontally
    private bool IsMouseMovingFast()
    {
        float currentMouseX = Input.mousePosition.x;
        float horizontalVelocity = (currentMouseX - lastMousePosition.x) / Time.deltaTime;
        lastMousePosition = Input.mousePosition;
        //Debug.Log(Mathf.Abs(horizontalVelocity));
        return Mathf.Abs(horizontalVelocity) >= horizontalVelocityThreshold;
    }

    public void Slice()
    {
        Collider[] hits = Physics.OverlapBox(cutPlane.position, new Vector3(5, 0.1f, 5), cutPlane.rotation, layerMask);

        if (hits.Length <= 0)
            return;

        for (int i = 0; i < hits.Length; i++)
        {
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
    }

    public SlicedHull SliceObject(GameObject obj, Material crossSectionMaterial = null)
    {
        // slice the provided object using the transforms of this object
        if (obj.GetComponent<MeshFilter>() == null)
            return null;

        return obj.Slice(cutPlane.position, cutPlane.up, crossSectionMaterial);
    }

    public void AddHullComponents(GameObject go)
    {
        go.layer = 9;
        Rigidbody rb = go.AddComponent<Rigidbody>();
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        MeshCollider collider = go.AddComponent<MeshCollider>();
        collider.convex = true;

        rb.AddExplosionForce(100, go.transform.position, 20);
    }
}
