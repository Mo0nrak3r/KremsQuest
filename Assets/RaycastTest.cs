using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class RaycastTest : MonoBehaviour
{

    // The prefab to instancieate on touch.
    [SerializeField]
    private GameObject _prefabToPlace;

    // Chache ARRaycastManager GameObject from ARCoreSession
    private ARRaycastManager _raycastManager;

    //List for Rraycast hits is re-used by raycast manager
    private static readonly List<ARRaycastHit> Hits = new List<ARRaycastHit>();


    void Awake()
    {
        _raycastManager = GetComponent<ARRaycastManager>();
    }

    // Update is called once per frame
    void Update()
    {   
        // Only consider single-Finger touches that are beginning
        Touch touch;
        if(Input.touchCount != 1 || (touch = Input.GetTouch(0)).phase != TouchPhase.Began)
        {
            return;
        }


        // Perform Raycast to any kind of trackable
        if(_raycastManager.Raycast(touch.position, Hits, UnityEngine.XR.ARSubsystems.TrackableType.All))
        {
            // Raycast hits are sorted by distance, so the first one will be the closest hit
            var hitPose = Hits[0].pose;

            // Instantiate prefab at the hit pose
            Instantiate(_prefabToPlace, hitPose.position, hitPose.rotation);

            Debug.Log($"Instanciated on: {Hits[0].hitType}");
        }
    }
}
