using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class DrivingSurfaceManager : MonoBehaviour
{
    public ARPlaneManager PlaneManager;
    public ARRaycastManager RaycastManager;
    public ARPlane LockedPlane;

    public void LockPlane(ARPlane keepPlane)
    {
        // Disable all planes except the one we want to keep
        var arPlane = keepPlane.GetComponent<ARPlane>();
        foreach (var plane in PlaneManager.trackables)
        {
            if (plane != arPlane)
            {
                plane.gameObject.SetActive(false);
            }
        }

        LockedPlane = arPlane;
        PlaneManager.planesChanged += DisableNewPlanes;
    }

    private void Start()
    {
        PlaneManager = GetComponent<ARPlaneManager>();
    }

    private void Update()
    {
        if (LockedPlane?.subsumedBy != null)
        {
            LockedPlane = LockedPlane.subsumedBy;
        }
    }

    private void DisableNewPlanes(ARPlanesChangedEventArgs args)
    {
        foreach (var plane in args.added)
        {
            plane.gameObject.SetActive(false);
        }
    }
}
