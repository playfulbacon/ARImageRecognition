using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleTrackedImageObjects : MonoBehaviour
{
    public GameObject trackedImageObjects;
    public Text buttonText;
    public bool editorOnStart;

    void Start()
    {
        if (!Application.isEditor)
        {
            SetAR(false);
        }
        else
        {
            SetAR(editorOnStart);
        }
    }

    void Update()
    {
        
    }

    public void Toggle()
    {
        bool set = !trackedImageObjects.activeSelf;
        SetAR(set);
    }

    public void SetAR(bool set)
    {
        trackedImageObjects.SetActive(set);
        buttonText.text = set ? "Hide AR" : "Show AR";
    }
}
