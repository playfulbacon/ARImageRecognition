using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;

public class ImageRecognition : MonoBehaviour
{
    public ARTrackedImageManager arTrackedImageManager;
    public Vector3 imageRotationOffset = new Vector3(90, 0, 0);
    public TrackedImage[] trackedImages;

    Dictionary<string, TrackedImage> trackedImageDictionary = new Dictionary<string, TrackedImage>();

    void Start()
    {
        arTrackedImageManager.maxNumberOfMovingImages = trackedImages.Length;
        DebugText.Instance.Log("maxNumberOfMovingImages set to " + arTrackedImageManager.maxNumberOfMovingImages);

        foreach (TrackedImage trackedImage in trackedImages)
        {
            trackedImageDictionary.Add(trackedImage.imageReferenceName, trackedImage);
            trackedImage.gameObject.SetActive(false);
            DebugText.Instance.Log(trackedImage.imageReferenceName + " added to dictionary");
        }
    }

    public void OnEnable()
    {
        arTrackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
        DebugText.Instance.Log("subscribed to trackedImagesChanged");
    }

    public void OnDisable()
    {
        arTrackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    public void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs args)
    {
        // removed
        foreach(ARTrackedImage trackedImage in args.removed)
        {
            DebugText.Instance.Log("removed " + trackedImage.referenceImage.name);
            SetARTrackedImageActive(trackedImage, false);
        }

        // added
        foreach (ARTrackedImage trackedImage in args.added)
        {
            DebugText.Instance.Log("added " + trackedImage.referenceImage.name);
            SetARTrackedImageActive(trackedImage, true);
            UpdateARImage(trackedImage);
        }

        // updated
        foreach (ARTrackedImage trackedImage in args.updated)
        {
            UpdateARImage(trackedImage);
        }
    }

    public void UpdateARImage(ARTrackedImage arTrackedImage)
    {
        string key = arTrackedImage.referenceImage.name;
        if (trackedImageDictionary.ContainsKey(key))
        {
            GameObject go = trackedImageDictionary[key].gameObject;
            go.transform.position = arTrackedImage.transform.position;

            Quaternion newRotation = arTrackedImage.transform.rotation;
            newRotation *= Quaternion.Euler(imageRotationOffset);
            go.transform.rotation = newRotation;
        }
    }

    public void SetARTrackedImageActive(ARTrackedImage arTrackedImage, bool set)
    {
        string key = arTrackedImage.referenceImage.name;
        if (trackedImageDictionary.ContainsKey(key))
        {
            TrackedImage trackedImage = trackedImageDictionary[key];

            if (set) trackedImage.AddImage();
            else trackedImage.RemoveImage();

            trackedImage.gameObject.SetActive(set);
        }
    }
}
