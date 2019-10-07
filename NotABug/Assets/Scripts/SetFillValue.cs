using UnityEngine;
using UnityEngine.UI;
using Fungus;

[CommandInfo("UI", 
            "Set Fill Value", 
            "Sets the fill value of a filled Image component.")]
[AddComponentMenu("")]
public class SetFillValue : Command
{
    [SerializeField] Image filledImage;
    [SerializeField] FloatData newFillValue = new FloatData(1f);

    public override void OnEnter()
    {
        base.OnEnter();

        filledImage.fillAmount = newFillValue.Value;
        Continue();
    }
}
