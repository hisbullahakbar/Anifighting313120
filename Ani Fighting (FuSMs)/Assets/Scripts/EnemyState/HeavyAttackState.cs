using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyAttackState : IEnemyState
{
    private Enemy enemy;

    private float heavyAttackTimer;
    private float heavyAttackCoolDown = 1;
    private bool canHeavyAttack = true;

    public string getStateName()
    {
        return "heavyAttack";
    }

    public void Enter(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public void Execute()
    {
        HeavyAttack();
    }

    public void Exit()
    {
    }

    public void OnTriggerEnter(Collider2D other)
    {
    }

    private void HeavyAttack()
    {
        if (canHeavyAttack)
        {
            canHeavyAttack = false;
            enemy.CharaAnimator.SetTrigger("heavyAttack");
        }
        heavyAttackTimer += Time.deltaTime;

        if (heavyAttackTimer >= heavyAttackCoolDown)
        {
            canHeavyAttack = true;
            heavyAttackTimer = 0;
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

            enemy.CharaAnimator.SetBool("crouch", false); //if before of this state chara is crouch attack (heavy)
        }
    }
}
