using UnityEngine;

public class EnemySpawnStateMachine : StateMachineBehaviour
{
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<Enemy>().Move();
    }
}
