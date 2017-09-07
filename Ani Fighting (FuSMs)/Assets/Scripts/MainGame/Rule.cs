using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rule : MonoBehaviour {
    //    idle,
    //    walk,
    //    walkBackward,
    //    lightAttack,
    //    heavyAttack,
    //    rangedAttack,
    //    jump,
    //    crouch

    [SerializeField]
    public MovementType.enemy[] ruleAfterIdle, ruleAfterWalk, ruleAfterWalkBackward, ruleAfterLightAttack, 
        ruleAfterHeavyAttack, ruleAfterRangedAttack, ruleAfterJump, ruleAfterCrouch;

    private static Rule instance;
    public static Rule Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<Rule>();
            }
            return instance;
        }
    }

    void Start()
    {

    }
}