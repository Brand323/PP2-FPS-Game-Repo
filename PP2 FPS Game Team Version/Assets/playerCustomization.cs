using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class playerCustomization : MonoBehaviour
{
    public TMP_InputField nameInputField; // Ref to player name input field

    public Renderer eyeRenderer; // The eye mesh renderer
    public Renderer beanRenderer;

    public Transform eyeTransform; // Transform for the eyes scaling
    public Slider eyeSizeSlider;
    private Vector3 defaultEyeScale; // Take original scale to store
    public TMP_Text eyeSizePercentageText;

    public Button redEyeButton, blueEyeButton, greenEyeButton, yellowEyeButton;

    public Button blueBeanButton, greenBeanButton, yellowBeanButton;

    // define colors for the eye
    private Color redColor = Color.red;
    private Color blueColor = Color.blue;
    private Color greenColor = Color.green;
    private Color yellowColor = Color.yellow;
    private Color blackColor = Color.black;

    void Start()
    {
        // Load saved name if available
        if (PlayerPrefs.HasKey("PlayerName"))
        {
            nameInputField.text = PlayerPrefs.GetString("PlayerName");
        }

        redEyeButton.onClick.AddListener(() => ChangeEyeColor(redColor));
        blueEyeButton.onClick.AddListener(() => ChangeEyeColor(blueColor));
        greenEyeButton.onClick.AddListener(() => ChangeEyeColor(greenColor));
        yellowEyeButton.onClick.AddListener(() => ChangeEyeColor(yellowColor));

        blueBeanButton.onClick.AddListener(() => ChangeBeanColor(blueColor));
        greenBeanButton.onClick.AddListener(() => ChangeBeanColor(greenColor));
        yellowBeanButton.onClick.AddListener(() => ChangeBeanColor(yellowColor));

        defaultEyeScale = eyeTransform.localScale;

        eyeSizeSlider.onValueChanged.AddListener(ChangeEyeSize);
        eyeSizeSlider.minValue = 0;
        eyeSizeSlider.maxValue = 1;
    }

    public void SavePlayerName()
    {
        // Save the name entered in the input field
        string playerName = nameInputField.text;
        PlayerPrefs.SetString("PlayerName", playerName);
        PlayerPrefs.Save();
    }
    public void ChangeEyeColor(Color newColor)
    {
        eyeRenderer.material.color = newColor;
    }

    public void ChangeBeanColor(Color newColor)
    {
        beanRenderer.material.color = newColor;
    }

    public void ChangeEyeSize(float sliderValue)
    {
        float scaleFactor = Mathf.Lerp(0.5f, 3f, sliderValue);
        eyeTransform.localScale = defaultEyeScale * scaleFactor;

        int percentageValue = Mathf.RoundToInt(sliderValue * 100);
        eyeSizePercentageText.text = percentageValue + "%";
    }
}
