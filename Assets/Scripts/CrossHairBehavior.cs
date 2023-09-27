using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(
    requiredComponent: typeof(ARRaycastManager),
    requiredComponent2: typeof(ARPlaneManager),
    requiredComponent3: typeof(DrivingSurfaceManager)
)]

public class CrossHairBehavior : MonoBehaviour
{

    [SerializeField] AudioClip spawnSound;

    public GameObject CrossHair;
    public GameObject CarPrefab;

    private ARRaycastManager aRRaycastManager;
    private ARPlaneManager aRPlaneManager;
    private DrivingSurfaceManager DrivingSurfaceManager;
    private ARPlane CurrentPlane;
    private GameObject Car;
    private CarBehaviour CarBehaviour;
    private AudioSource audioSource;
    private UIInstructionMenu ui;
    private UIHud HUD;
    
    private void Awake() {
        aRRaycastManager = GetComponent<ARRaycastManager>();
        aRPlaneManager = GetComponent<ARPlaneManager>();
        DrivingSurfaceManager = GetComponent<DrivingSurfaceManager>();
        CurrentPlane = GetComponent<ARPlane>();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        // get the CrossHair prefab
        CrossHair = transform.GetChild(0).gameObject;
        audioSource = GetComponent<AudioSource>();
        ui = FindObjectOfType<UIInstructionMenu>();
        HUD = FindObjectOfType<UIHud>();
    }

    // Update is called once per frame
    void Update()
    {
        CurrentPlane = null;

        var screenCenter = Camera.main.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var hits = new List<ARRaycastHit>();
        aRRaycastManager.Raycast(screenCenter, hits, TrackableType.PlaneWithinBounds);

        ARRaycastHit? hit = null;
        if (hits.Count > 0)
        {
            // If we don't have a locked plane already
            var lockedPlane = DrivingSurfaceManager.LockedPlane;
            hit = null;
            
            if (lockedPlane == null)
            {
                // use the first hit in `hits`.
                hit = hits[0];
            }
            else
            {   
                // Otherwise we use the locked plane, if it's there.
                foreach (var h in hits)
                {
                    if (h.trackableId == lockedPlane.trackableId)
                    {
                        hit = h;
                        break; // Once we find a match, exit the loop.
                    }
                }
            }
        }

        if (hit.HasValue)
        {
            CurrentPlane = aRPlaneManager.GetPlane(hit.Value.trackableId);
            DrivingSurfaceManager.LockPlane(CurrentPlane);
            // Move this reticle to the location of the hit.
            CrossHair.transform.position = hit.Value.pose.position;

            // Hide instructions for Step One and Show Instructions for Step 2 + show HUD
            ui.updateTextToStepTwo();
            HUD.ShowHUD();
            
        }

        CrossHair.SetActive(CurrentPlane != null);

        if (Car == null && WasTapped() && CurrentPlane)
        {
            spawnCar();
        }
    }
    // Spawn our car at the crosshair location.
    private void spawnCar () {
        Car = Instantiate(original: CarPrefab);
        CarBehaviour = Car.GetComponent<CarBehaviour>();
        CarBehaviour.CrossHair = CrossHair;
        Car.transform.position = CrossHair.transform.position;

        // UI Managements
        ui.hideInstructions();
        HUD.startTimer();

        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(spawnSound);
        }
    }

    private bool WasTapped()
    {
        if (Input.GetMouseButtonDown(0))
        {
            return true;
        }

        if (Input.touchCount == 0)
        {
            return false;
        }
        var touch = Input.GetTouch(0);
        if (touch.phase != TouchPhase.Began)
        {
            return false;
        }

        return true;
    }
}
