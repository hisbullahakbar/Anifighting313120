using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyAttackStateFSM : IEnemyState
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

            enemy.CharaAnimator.SetBool("crouch", false); //if before of this state chara is crouch attack (heavy)
        }
    }
}
