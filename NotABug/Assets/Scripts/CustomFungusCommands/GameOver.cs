using System;
using UnityEngine;
using Fungus;

[CommandInfo("Platformer",
                 "Game Over",
                 "Executes all blocks that work off the OnGameOver event.")]
[AddComponentMenu("")]
public class GameOver : Command
{
    public override void OnEnter()
    {
        OnGameOver.Invoke();
        Continue();
    }
}
