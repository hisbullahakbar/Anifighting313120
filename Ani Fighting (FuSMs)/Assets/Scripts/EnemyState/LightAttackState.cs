using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightAttackState : IEnemyState
{
    private Enemy enemy;

    private float lightAttackTimer;
    private float lightAttackCoolDown = 0; //4f
    private bool canLightAttack = true;

    public string getStateName()
    {
        return "lightAttack";
    }

    public void Enter(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public void Execute()
    {
        LightAttack();
    }

    public void Exit()
    {
    }

    public void OnTriggerEnter(Collider2D other)
    {
    }

    private void LightAttack()
    {
        if (canLightAttack)
        {
            canLightAttack = false;
            enemy.CharaAnimator.SetTrigger("lightAttack");
        }
        lightAttackTimer += Time.deltaTime;

        if (lightAttackTimer >= lightAttackCoolDown)
        {
            canLightAttack = true;
            lightAttackTimer = 0;
            //enemy.ChangeState(new IdleState());
            
            FuzzyStateMachines.Instance.initiateFuSMs();
            FuzzyStateMachines.Instance.runFuSMs();

            switch (((MovementType.enemy)FuzzyStateMachines.Instance.ChoosenRuleIndex).ToString())
            {
                //masih bug sedikit disini
                case "idle":
                    enemy.ChangeState(new IdleState());
                    break;
                case "walk":
                    enemy.ChangeState(new WalkState());
                    break;
                case "walkBackward":
                    enemy.ChangeState(new WalkBackwardState());
                    break;
                case "lightAttack":
                    enemy.ChangeState(new LightAttackState());
                    break;
                case "heavyAttack":
                    enemy.ChangeState(new HeavyAttackState());
                    break;
                case "rangedAttack":
                    enemy.ChangeState(new RangedAttackState());
                    break;
                case "jump":
                    enemy.ChangeState(new JumpState());
                    break;
                case "crouch":
                    enemy.ChangeState(new CrouchState());
                    break;
            }
        }
    }
}
