using UnityEngine;
using Fungus;

[CommandInfo("Variable", 
            "Set As DeltaTime", 
            "Sets the selected FloatVariable as equal to Time.deltaTime.")]
[AddComponentMenu("")]
public class SetAsDeltaTime : Command
{
    [SerializeField] FloatData varToSet;
    public override void OnEnter()
    {
        base.OnEnter();
        varToSet.Value = Time.deltaTime;
    }
}
