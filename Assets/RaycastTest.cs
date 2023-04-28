using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class RaycastTest : MonoBehaviour
{

    ARAnchor CreateAnchor(in ARRaycastHit hit)
    {
        ARAnchor anchor;
        // Get the trackable ID in case the raycast hit a trackable
        //var hitTrackableId = hit.trackableId;

        // Attempt to retrieve a plane if the trackable is of type plane
        // and the the raycast hit one
        //var hitPlane = _planeManager.GetPlane(hitTrackableId);

        if (hit.trackable is ARPlane hitPlane)
        {
            // The raycast hit a plane - therefore, attach the anchor to the plane.
            // According to the AR Foundation documentation:
            // Attaching an anchor to a plane affects the anchor update semantics.
            // This type of anchor only changes its position along the normal of
            // the plane to which it is attached,
            // thus maintaining a constant distance from the plane.

            // When the Anchor Manager has a prefab assigned to its property,
            // it will instantiate that and automatically make it a child
            // of an anchor GameObject.
            // The following code temporarily replaces the default prefab
            // with the one we want to instantiate from our script, to ensure
            // it doesn't interfere with potential other logic in your app.
            var oldPrefab = _anchorManager.anchorPrefab;
            _anchorManager.anchorPrefab = _prefabToPlace;
            anchor = _anchorManager.AttachAnchor(hitPlane, hit.pose);
            _anchorManager.anchorPrefab = oldPrefab;

            // Note: the following method seems to produce an offset when placing
            // the prefab instance in AR Foundation 5.0 pre 8
            //anchor = _anchorManager.AttachAnchor(hitPlane, hit.pose);
            // Make our prefab a child of the anchor, so that it's moved
            // with that anchor.
            //Instantiate(_prefabToPlace, anchor.transform);

            Debug.Log($"Created anchor attachment for plane (id: {anchor.nativePtr}).");
        }
        else
        {
            // Otherwise, just create a regular anchor at the hit pose
            // Note: the anchor can be anywhere in the scene hierarchy
            var instantiatedObject = Instantiate(_prefabToPlace, hit.pose.position, hit.pose.rotation);

            // Make sure the new GameObject has an ARAnchor component.
            if (!instantiatedObject.TryGetComponent<ARAnchor>(out anchor))
            {
                // If the prefab doesn't include the ARAnchor component,
                // simply add it.
                // Note: ARAnchorManager.AddAnchor() is obsolete, this
                // is the way to go! ARAnchor will add itself to the
                // anchor manager once it is enabled.
                anchor = instantiatedObject.AddComponent<ARAnchor>();
            }
            Debug.Log($"Created regular anchor (id: {anchor.nativePtr}).");
        }

        return anchor;
    }

    // The prefab to instancieate on touch.
    [SerializeField]
    private GameObject _prefabToPlace;

    // Chache ARRaycastManager GameObject from ARCoreSession
    private ARRaycastManager _raycastManager;

    // Cache ARAnchorManager GameObject from XROrigin
    private ARAnchorManager _anchorManager;

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
            //Instantiate(_prefabToPlace, hitPose.position, hitPose.rotation);

            // Create anchor
            CreateAnchor(Hits[0]);

            Debug.Log($"Instanciated on: {Hits[0].hitType}");
        }
    }
}
