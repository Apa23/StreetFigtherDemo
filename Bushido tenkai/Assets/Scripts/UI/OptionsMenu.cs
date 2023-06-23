using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;
public class OptionsMenu : MonoBehaviour
{
    // Interactive controls variables
    Toggle _toggle;
    Slider _slider;
    TMP_Dropdown _dropdown;

    // Audio source variables
    [SerializeField] 
    private AudioSource _audioSource; 



    private void Start()
    {
        _toggle = gameObject.GetComponentInChildren<Toggle>();
        _slider = gameObject.GetComponentInChildren<Slider>();
        _dropdown = gameObject.GetComponentInChildren<TMP_Dropdown>();

        // Listener for interactive controls events
        if (_toggle != null)
        {
            _toggle.onValueChanged.AddListener(delegate
                   {
                       FullScreen(_toggle.isOn);
                   });
        }
        if (_slider != null)
        {
            _slider.onValueChanged.AddListener(delegate
                   {
                       ChangeVolumen(_slider.value);
                   });

        }
        if (_dropdown != null)
        {
            Debug.Log("Dropdown");
            _dropdown.onValueChanged.AddListener(delegate
                   {
                       ChangeQuality(_dropdown.value);
                   });

        }
    }

    // Change screen mode
    public void FullScreen(bool fullScreen)
    {
        if (fullScreen)
        {
            Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
            _toggle.gameObject.GetComponentsInChildren<Image>()[1].enabled = fullScreen;
        }
        else
        {
            Screen.fullScreenMode = FullScreenMode.Windowed;
            _toggle.gameObject.GetComponentsInChildren<Image>()[1].enabled = fullScreen;
        }
    }

    // Change the volume of music
    public void ChangeVolumen(float volumen)
    {
        _audioSource.volume = volumen;
    }

    // Change the game Quality
    public void ChangeQuality(int index)
    {
        QualitySettings.SetQualityLevel(index);
    }
}
