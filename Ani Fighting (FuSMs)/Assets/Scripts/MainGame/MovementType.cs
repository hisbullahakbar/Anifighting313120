using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementType : MonoBehaviour {

    public enum enemy
    {
        idle,
        walk,
        walkBackward,
        lightAttack,
        heavyAttack,
        rangedAttack,
        jump,
        crouch
    };

    public enum playerCounter
    {
        lightAttack,
        heavyAttack,
        rangedAttack,
        upDirection,
        middleDirection,
        bottomDirection
    }
}
