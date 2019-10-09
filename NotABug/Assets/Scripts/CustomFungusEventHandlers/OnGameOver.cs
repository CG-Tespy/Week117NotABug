using UnityEngine;
using Fungus;

/// <summary>
    /// The block will execute when the user toggles on the target UI toggle object.
    /// </summary>
    [EventHandlerInfo("Platformer",
                      "On Death",
                      "The block will execute when the specified Damageable dies.")]
    [AddComponentMenu("")]
public class OnGameOver : EventHandler
{

    public static void Invoke()
    {
        foreach (var eventHandler in GameObject.FindObjectsOfType<OnGameOver>())
        {
            eventHandler.ExecuteBlock();
        }
    }
}
