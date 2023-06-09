using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARTrackedImageManager))]
public class ARPlaceTrackedImages : MonoBehaviour
{
    // Cache AR tracked images manager from ARCoreSession
    private ARTrackedImageManager _trackedImagesManager;

    public GameObject[] ArPrefabs;

    private readonly Dictionary<string, GameObject> _instantiatedPrefabs = new Dictionary<string, GameObject>();

    // Reference to logging UI element in the canvas
    public UnityEngine.UI.Text Log;
    
    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs) 
    {
        // Go through all tracked images that have been added 
        // (-> new markers detected) 
        foreach (var trackedImage in eventArgs.added) 
        { 
            // Get the name of the reference image to search for the corresponding prefab 
            var imageName = trackedImage.referenceImage.name; 
            
            foreach (var curPrefab in ArPrefabs) 
            { 
                if (string.Compare(curPrefab.name, imageName, StringComparison.Ordinal) == 0 
                    && !_instantiatedPrefabs.ContainsKey(imageName)) 
                { 
                    // Found a corresponding prefab for the reference image, and it has not been 
                    // instantiated yet > new instance, with the ARTrackedImage as parent 
                    // (so it will automatically get updated when the marker changes in real life) 
                    var newPrefab = Instantiate(curPrefab, trackedImage.transform); 
                    // Store a reference to the created prefab 
                    _instantiatedPrefabs[imageName] = newPrefab;
                    Log.text = $"{Time.time} -> Instantiated prefab for tracked image (name: {imageName}).\n" +
                               $"newPrefab.transform.parent.name: {newPrefab.transform.parent.name}.\n" +
                               $"guid: {trackedImage.referenceImage.guid}";
                    ShowAndroidToastMessage("Instantiated!");
                } 
            } 
        }
        // Disable instantiated prefabs that are no longer being actively tracked
        foreach (var trackedImage in eventArgs.updated) { 
            _instantiatedPrefabs[trackedImage.referenceImage.name] 
                .SetActive(trackedImage.trackingState == TrackingState.Tracking); 
        } 

        // Remove is called if the subsystem has given up looking for the trackable again.
        // (If it's invisible, its tracking state would just go to limited initially).
        // Note: ARCore doesn't seem to remove these at all; if it does, it would delete our child GameObject
        // as well.
        foreach (var trackedImage in eventArgs.removed) { 
            // Destroy the instance in the scene.
            // Note: this code does not delete the ARTrackedImage parent, which was created
            // by AR Foundation, is managed by it and should therefore also be deleted by AR Foundation.
            Destroy(_instantiatedPrefabs[trackedImage.referenceImage.name]);
            // Also remove the instance from our array
            _instantiatedPrefabs.Remove(trackedImage.referenceImage.name);

            // Alternative: do not destroy the instance, just set it inactive
            //_instantiatedPrefabs[trackedImage.referenceImage.name].SetActive(false);

            Log.text = $"REMOVED (guid: {trackedImage.referenceImage.guid}).";
        }

    }
    // Cache AR tracked images manager from ARCoreSession private ARTrackedImageManager _trackedImagesManager;
    void Awake() {
        _trackedImagesManager = GetComponent<ARTrackedImageManager>();
    }

    void OnEnable() { 
        _trackedImagesManager.trackedImagesChanged += OnTrackedImagesChanged; 
    } 

    void OnDisable() { 
        _trackedImagesManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    

    /// <summary>
    /// Show an Android toast message.
    /// </summary>
    /// <param name="message">Message string to show in the toast.</param>
    private static void ShowAndroidToastMessage(string message)
    {
#if UNITY_ANDROID
        using var unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        var unityActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        if (unityActivity == null) return;
        var toastClass = new AndroidJavaClass("android.widget.Toast");
        unityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
        {
            // Last parameter = length. Toast.LENGTH_LONG = 1
            using var toastObject = toastClass.CallStatic<AndroidJavaObject>("makeText",
                unityActivity, message, 1);
            toastObject.Call("show");
        }));
#endif
    }
}
