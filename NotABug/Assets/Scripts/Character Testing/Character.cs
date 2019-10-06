using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Character")]
public class Character : ScriptableObject
{
    public new string name;
    public string aboutMe;
    public Sprite artwork;
    public Rigidbody2D rigidBody;
    public SpecialAbility.Type specialAbility;
    public Transform transform;

    private bool performActionInFixedUpdate = false;

    public void Update()
    {
        if (!SpecialAbility.IsTypePhysicsBased(specialAbility))
        {
            SpecialAbility.Perform(this);
        }
        else
        {
            performActionInFixedUpdate = true;
        }
    }

    public void FixedUpdate()
    {
        if (performActionInFixedUpdate)
        {
            if (SpecialAbility.IsTypePhysicsBased(specialAbility))
            {
                SpecialAbility.Perform(this);
            }

            performActionInFixedUpdate = false;
        }

        return;
    }
}
