using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// When a given slider's value changes, a given AudioSource's volume changes to match.
/// </summary>
public class SyncVolumeToSliderValue : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] Slider slider;

    void Awake()
    {
        slider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    void OnDestroy()
    {
        slider.onValueChanged.RemoveListener(OnSliderValueChanged);
    }

    void OnSliderValueChanged(float newSliderValue)
    {
        audioSource.volume = newSliderValue;
    }
}
