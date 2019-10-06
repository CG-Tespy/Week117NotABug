using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Fungus;

/// <summary>
    /// The block will execute when the user toggles on the target UI toggle object.
    /// </summary>
    [EventHandlerInfo("UI",
                      "Slider Value Changed",
                      "The block will execute when the value of the target UI slider object changes.")]
    [AddComponentMenu("")]
public class SliderValueChanged : EventHandler
{
    [SerializeField] protected Slider targetSlider;
    protected float SliderValue { get { return targetSlider.value; } }

    [Tooltip("Variable that the new slider value gets stored in.")]
    [VariablePropertyAttribute(typeof(FloatVariable))] 
    [SerializeField] protected FloatVariable sliderValue;

    void Awake()
    {
        //VariablePropertyAttribute
        if (targetSlider != null)
            ListenForSliderValueChange();
    }

    void ListenForSliderValueChange()
    {
        targetSlider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    protected virtual void OnSliderValueChanged(float newValue)
    {
        if (targetSlider != null)
        {
            if (sliderValue != null)
                sliderValue.Value = newValue;

            ExecuteBlock();
        }
    }

}
