using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using EnhancedTouch = UnityEngine.InputSystem.EnhancedTouch;
[RequireComponent(requiredComponent: typeof(ARRaycastManager), requiredComponent2: typeof(ARPlaneManager))]
public class PlaceObject : MonoBehaviour
{
    [SerializeField] private GameObject prefab1;
    [SerializeField] private GameObject prefab2;

    private ARRaycastManager aRRaycastManager;
    private ARPlaneManager aRPlaneManager;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();
    public float maxRotationAngle = 120f; // Maximum rotation angle in degrees

    // Define the range of scales you want to apply to the instantiated object
    // public float minScale = 70.0f;
    // public float maxScale = 100.0f;

    private void Awake() {
        aRRaycastManager = GetComponent<ARRaycastManager>();
        aRPlaneManager = GetComponent<ARPlaneManager>();
    }

    private void OnEnable() {
        EnhancedTouch.TouchSimulation.Enable();
        EnhancedTouch.EnhancedTouchSupport.Enable();

        EnhancedTouch.Touch.onFingerDown += FingerDown;
    }

    private void OnDisable() {
        EnhancedTouch.TouchSimulation.Disable();
        EnhancedTouch.EnhancedTouchSupport.Disable();

        EnhancedTouch.Touch.onFingerDown -= FingerDown;
    }

    private void FingerDown (EnhancedTouch.Finger finger) {
        if (finger.index != 0) return;

        if (aRRaycastManager.Raycast(finger.currentTouch.screenPosition, hits, TrackableType.PlaneWithinPolygon)) {
            foreach (ARRaycastHit hit in hits)
            {   
                Pose pose = hit.pose;
                
                // Generate a random number between 0 (inclusive) and 2 (exclusive)
                int randomPrefabIndex = Random.Range(0, 2);

                // Decide which prefab to instantiate based on the random number
                GameObject prefabToInstantiate = randomPrefabIndex == 0 ? prefab1 : prefab2;

                // Randomly rotate the prefab around the y-axis
                Quaternion randomRotation = Quaternion.Euler(0f, Random.Range(-maxRotationAngle, maxRotationAngle), 0f);

                // Instantiate the chosen prefab at the given position with the random rotation
                GameObject obj = Instantiate(original: prefabToInstantiate, position: pose.position, rotation: pose.rotation * randomRotation);

                // Apply the random scale to the instantiated object
                // obj.transform.localScale = new Vector3(randomScale, randomScale, randomScale);

                // if (aRPlaneManager.GetPlane(trackableId: hit.trackableId).alignment == PlaneAlignment.HorizontalUp) {
                //     Vector3 position = obj.transform.position;
                //     position.y = 0f;
                //     Vector3 cameraPosition = Camera.main.transform.position;
                //     cameraPosition.y = 0f;
                //     Vector3 direction = cameraPosition - position;
                //     Vector3 targetRotationEuler = Quaternion.LookRotation(forward: direction).eulerAngles;

                //     Vector3 scaledEuler = Vector3.Scale(a: targetRotationEuler, b: obj.transform.up.normalized ); // (0, 1, 0)

                //     Quaternion targetRotation = Quaternion.Euler(euler: scaledEuler);
                //     obj.transform.rotation = obj.transform.rotation * targetRotation;
                // }
            }
        }
    }
}
