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

    private bool performActionInFixedUpdate = false;

    public void AttemptAbilityFromUpdate()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!SpecialAbility.IsTypePhysicsBased(specialAbility))
            {
                Debug.Log("Performing action in Update");
                SpecialAbility.Perform(this);
            }
            else
            {
                performActionInFixedUpdate = true;
            }
        }

    }

    public void AttemptAbilityFromFixedUpdate()
    {
        if (performActionInFixedUpdate)
        {
            if (SpecialAbility.IsTypePhysicsBased(specialAbility))
            {
                Debug.Log("Performing action in FixedUpdate");
                SpecialAbility.Perform(this);
            }

            performActionInFixedUpdate = false;
        }

        return;
    }
}
