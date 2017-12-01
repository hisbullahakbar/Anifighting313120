using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightAttackStateFSM : IEnemyState
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

            FiniteStateMachines.Instance.initiateFSM();
            FiniteStateMachines.Instance.runFSM();

            switch (((MovementType.enemy)FiniteStateMachines.Instance.ChoosenRuleIndex).ToString())
            {
                //masih bug sedikit disini
                case "idle":
                    enemy.ChangeState(new IdleStateFSM());
                    break;
                case "walk":
                    enemy.ChangeState(new WalkStateFSM());
                    break;
                case "walkBackward":
                    enemy.ChangeState(new WalkBackwardStateFSM());
                    break;
                case "lightAttack":
                    enemy.ChangeState(new LightAttackStateFSM());
                    break;
                case "heavyAttack":
                    enemy.ChangeState(new HeavyAttackStateFSM());
                    break;
                case "rangedAttack":
                    enemy.ChangeState(new RangedAttackStateFSM());
                    break;
                case "jump":
                    enemy.ChangeState(new JumpStateFSM());
                    break;
                case "crouch":
                    enemy.ChangeState(new CrouchStateFSM());
                    break;
            }
        }
    }
}
