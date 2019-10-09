using UnityEngine;
using Fungus;

[CommandInfo("Rigidbody2D",
                 "SetVelocity2D",
                 "Set a Rigidbody2D's velocity")]
[AddComponentMenu("")]
public class SetVelocity2D : Command
{
    [SerializeField] new Rigidbody2D rigidbody2D;

    [SerializeField] protected Vector2Data velocity;

    public override void OnEnter()
    {
        base.OnEnter();

        rigidbody2D.velocity = velocity.Value;
        Continue();
    }
}
