using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TrackedImage : MonoBehaviour
{
    public string imageReferenceName;
    Vector3 startRotation = new Vector3(90, 0, 0);
    public Transform scaler;
    public GameObject image;
    public bool showImage = false;
    public event Action OnImageAdded;
    public event Action OnImagedRemoved;

    void Start()
    {
        if (!Application.isEditor)
            Setup();
    }

    void Setup()
    {
        image.SetActive(showImage);
    }

    public void AddImage()
    {
        FindObjectOfType<DebugImageScaler>().SetCurrentImage(this);
        OnImageAdded?.Invoke();
    }

    public void RemoveImage()
    {
        OnImagedRemoved?.Invoke();
    }
}
