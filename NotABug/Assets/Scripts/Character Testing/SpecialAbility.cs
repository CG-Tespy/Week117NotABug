﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAbility
{
    public enum Type { None, Fly, Lift, PickLock };

    static public void Perform(Character character)
    {
        switch (character.specialAbility)
        {
            case Type.Fly:
            {
                Fly(character);
                break;
            }
            case Type.Lift:
            {
                Lift(character);
                break;
            }
            case Type.PickLock:
            {
                PickLock(character);
                break;
            }
            default:
            {
                Debug.Log("One day I will be special.");
                break;
            }
        }
    }
    
    static public bool IsTypePhysicsBased(Type actionType)
    {
        switch (actionType)
        {
            case Type.Fly:
            case Type.Lift:
            {
                return true;
            }
            case Type.PickLock:
            default:
            {
                return false;
            }
        }
    }

    static private void Fly(Character character)
    {
        Debug.Log("I always knew I could fly...");
        character.rigidBody.position = character.rigidBody.position + Vector2.one;
    }

    static private void Lift(Character character)
    {
        Debug.Log("I am trying to lift something.");
        character.rigidBody.position = character.rigidBody.position + Vector2.up;
    }

    static private void PickLock(Character character)
    {
        Debug.Log("I am attempting to pick a lock!");
        character.rigidBody.position = character.rigidBody.position + Vector2.left;
    }
}
