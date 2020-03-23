//script provided by unity https://github.com/Unity-Technologies/arfoundation-samples/blob/master/Assets/Scenes/ImageTracking/TrackedImageInfoManager.cs

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.ARFoundation;

/// This component listens for images detected by the <c>XRImageTrackingSubsystem</c>
/// and overlays some information as well as the source Texture2D on top of the
/// detected image.
/// </summary>
[RequireComponent(typeof(ARTrackedImageManager))]
public class ImageTracking : MonoBehaviour
{
     private Camera _camera;

    private ARTrackedImageManager _arTrackedImageManager;
    private ARTrackedImage _trackedImage;
    // Start is called before the first frame update
    void Start()
    {
        _camera = FindObjectOfType<Camera>();
    }

    void Awake() {
        _arTrackedImageManager = FindObjectOfType<ARTrackedImageManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void onEnable() {
        _arTrackedImageManager.trackedImagesChanged += OnImageChanged;
        
    }

    public void onDisable() {
        _arTrackedImageManager.trackedImagesChanged -= OnImageChanged;
    }

    public void OnImageChanged(ARTrackedImagesChangedEventArgs args) {
        //Debug.Log("AVATAR INSTANTIATED");
        foreach (var trackedImage in args.added) {
            Debug.Log(_arTrackedImageManager.name);
        }
    }
}
