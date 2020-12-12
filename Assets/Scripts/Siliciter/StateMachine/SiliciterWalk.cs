using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SiliciterWalk : StateMachineBehaviour
{
    //float interval = 3f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        EnemyMove temp = animator.GetComponent<EnemyMove>();
        if (temp != null)
        {
            temp.IntervalWalk();
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //if(interval == 3f)
        //{
        //    EnemyMove temp = animator.GetComponent<EnemyMove>();
        //    if(temp != null)
        //    {
        //        temp.IntervalWalk();
        //    }
        //}

        //interval -= Time.deltaTime;
        //if (interval <= 0)
        //    interval = 3f;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Implement code that processes and affects root motion
    }

    // OnStateIK is called right after Animator.OnAnimatorIK()
    override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Implement code that sets up animation IK (inverse kinematics)
    }
}
