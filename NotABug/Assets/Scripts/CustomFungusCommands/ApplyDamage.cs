using UnityEngine;
using Fungus;

[CommandInfo("Platformer",
                 "Apply Damage",
                 "Makes the damageable take a certain amount of damage.")]
[AddComponentMenu("")]
public class ApplyDamage : Command
{
    [SerializeField] Damageable damageable;
    [SerializeField] FloatData damage = new FloatData(1f);

    public override void OnEnter()
    {
        if (damageable != null)
            damageable.TakeDamage(damage.Value);
        Continue();
    }
}
