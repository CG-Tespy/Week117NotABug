using UnityEngine;
using Fungus;

/// <summary>
    /// The block will execute when the specified Damageable dies.
    /// </summary>
    [EventHandlerInfo("Platformer",
                      "On Death",
                      "The block will execute when the specified Damageable dies.")]
    [AddComponentMenu("")]
public class OnDeath : EventHandler
{
    [SerializeField] Damageable damageable;

    void Awake()
    {
        damageable.Death += OnDamageableDeath;
    }

    void OnDestroy()
    {
        damageable.Death -= OnDamageableDeath;
    }

    void OnDamageableDeath()
    {
        ExecuteBlock();
    }
}
