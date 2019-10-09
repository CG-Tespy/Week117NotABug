using UnityEngine;

using Fungus;

[CommandInfo("Rigidbody2D",
                 "GetVelocity2D",
                 "Stores a Rigidbody2D's velocity into a Vector2Variable.")]
[AddComponentMenu("")]
public class GetVelocity2D : Command
{
    [SerializeField] new Rigidbody2D rigidbody2D;
    [SerializeField] Vector2Data vector2;

    public override void OnEnter()
    {
        vector2.Value = rigidbody2D.velocity;
        Continue();
    }
}
