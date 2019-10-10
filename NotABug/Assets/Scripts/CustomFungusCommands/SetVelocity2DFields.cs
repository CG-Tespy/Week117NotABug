using UnityEngine;

using Fungus;

// Credit to original Fungus devs for this code

[CommandInfo("Rigidbody2D",
                 "SetVelocity2DFields",
                 "Set a Rigidbody2D's velocity")]
[AddComponentMenu("")]
public class SetVelocity2DFields : Command
{
    [SerializeField] protected Vector2Data vector2;
    [SerializeField] protected FloatData x, y;

    public override void OnEnter()
    {
        vector2.Value.Set(x.Value, y.Value);
        Continue();
    }

}
