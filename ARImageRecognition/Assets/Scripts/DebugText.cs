using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugText : MonoBehaviour
{
    public static DebugText Instance;
    public Button debugButton;
    public Text debugText;
    Text buttonText;
    public GameObject debugHolder;
    bool showDebug = false;
    string text = "";

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        buttonText = debugButton.GetComponentInChildren<Text>();
        debugText.text = "";
        SetDebug(showDebug);
    }

    void Update()
    {
        
    }

    public void OnDebugButtonPress()
    {
        SetDebug(!showDebug);
    }

    void SetDebug(bool set)
    {
        showDebug = set;
        debugHolder.SetActive(set);
        buttonText.text = set ? "Hide Debug" : "Show Debug";
    }

    public void Log(string log)
    {
        text = log + "\n" + text;
        debugText.text = text;
    }
}
