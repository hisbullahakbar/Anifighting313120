using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBehaviour : StateMachineBehaviour {

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        animator.GetComponent<Character>().attack = true;
        animator.SetFloat("speed", 0f);

        if (animator.tag == "Player")
        {
            if (Player.Instance.onGround)
            {
                Player.Instance.charaRigidbody2D.velocity = Vector2.zero;
            }
        }
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	//override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        //if (!Player.Instance.onGround)
        //{
        //    animator.SetBool("land", true);
        //}
	//}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<Character>().attack = false;
        for (int i = 0; i < animator.GetComponent<Character>().lightAttackCollider.Length; i++)
        {
            animator.GetComponent<Character>().setLightAttackCollider(i);
        }

        animator.ResetTrigger("lightAttack");
        animator.ResetTrigger("rangedAttack");
        animator.ResetTrigger("heavyAttack");
        animator.ResetTrigger("crouchAttack");
    }

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
}
