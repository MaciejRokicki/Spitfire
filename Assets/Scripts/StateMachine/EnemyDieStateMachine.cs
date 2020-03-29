using UnityEngine;

public class EnemyDieStateMachine : StateMachineBehaviour
{
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<Enemy>().Destroy();
    }
}
