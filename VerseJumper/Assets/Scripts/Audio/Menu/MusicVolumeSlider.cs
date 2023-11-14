using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicVolumeSlider : MonoBehaviour
{
    public Slider volumeSlider;

    private MenuAudioManager audioManager;

    private void Start()
    {
        // Find the MenuAudioManager in the scene
        audioManager = FindObjectOfType<MenuAudioManager>();

        if (audioManager != null && volumeSlider != null)
        {
            // Add a listener to the slider's value changed event
            volumeSlider.onValueChanged.AddListener(OnVolumeChanged);

            // Set the initial volume based on the slider value
            OnVolumeChanged(volumeSlider.value);
        }
        else
        {
            Debug.LogWarning("MenuAudioManager or volumeSlider not found.");
        }
    }

    private void OnVolumeChanged(float sliderValue)
    {
        // Set the music volume using MenuAudioManager's SetMusic function
        if (audioManager != null)
        {
            audioManager.SetMusic(sliderValue);
        }
    }
}
