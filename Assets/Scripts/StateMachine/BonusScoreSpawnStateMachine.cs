using UnityEngine;

namespace Assets.Scripts.StateMachine
{
    public class BonusScoreSpawnStateMachine : StateMachineBehaviour
    {
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.GetComponent<BonusScore>().Move();
        }
    }
}
