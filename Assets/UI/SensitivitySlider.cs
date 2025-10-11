using UnityEngine;
using UnityEngine.UI;
public class SensitivitySlider : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public Slider sensitivitySlider;
    public Text sensitivityValueText;
    void Start()
    {
        float savedSensitivity = PlayerPrefs.GetFloat("Sensitivity", 2f);
        sensitivitySlider.value = savedSensitivity;
    }

    public void ApplySensitivity(float newValue)
    {
        if (sensitivityValueText != null)
        {
            sensitivityValueText.text = newValue.ToString("F1");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
