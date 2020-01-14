using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugImageScaler : MonoBehaviour
{
    public Slider scaleSlider;
    public Text scaleText;
    Transform imageToScale;
    TrackedImage currentImage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetCurrentImage(TrackedImage trackedImage)
    {
        currentImage = trackedImage;
    }

    public void OnScaleSliderChanged()
    {
        if (currentImage)
        {
            float scale = scaleSlider.value;
            imageToScale = currentImage.scaler;
            imageToScale.localScale = new Vector3(scale, scale);
            scaleText.text = scale.ToString();
        }
    }
}
