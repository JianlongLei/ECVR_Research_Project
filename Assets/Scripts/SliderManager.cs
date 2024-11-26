using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Reflection;

public class SliderManager : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI valueDisplayText;
    public string configName;
    private AppConfig config;

    void Start()
    {
        config = ConfigManager.LoadConfig("Assets/revolve2/vr/db/config.json");
        if (config != null && !string.IsNullOrWhiteSpace(configName))
        {
            // Use reflection to get the property value by name
            PropertyInfo propertyInfo = config.GetType().GetProperty(configName);
            if (propertyInfo != null)
            {
                object value = propertyInfo.GetValue(config, null);
                if (value != null)
                {
                    valueDisplayText.text = value.ToString();
                    slider.value = ConvertToSliderValue(value);
                }
            }
            else
            {
                Debug.LogError($"Property {configName} not found on AppConfig.");
            }
        }
        else
        {
            Debug.LogError("Config is null or configName is empty.");
        }
        if (slider != null && valueDisplayText != null)
        {
            slider.onValueChanged.AddListener(UpdateValueDisplay);
        }
    }

    private void UpdateValueDisplay(float value)
    {
        if (valueDisplayText != null)
        {
            valueDisplayText.text = Mathf.RoundToInt(value).ToString();
        }
    }

    private float ConvertToSliderValue(object configValue)
    {
        float sliderValue = 0;
        if (float.TryParse(configValue.ToString(), out sliderValue))
        {
            return sliderValue;
        }
        return 1; // Default to 1 if parsing fails
    }

    void OnDestroy()
    {
        if (slider != null)
        {
            slider.onValueChanged.RemoveListener(UpdateValueDisplay);
        }
    }
}
