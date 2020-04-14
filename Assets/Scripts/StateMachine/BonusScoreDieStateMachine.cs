using UnityEngine;

namespace Assets.Scripts.StateMachine
{
    public class BonusScoreDieStateMachine : StateMachineBehaviour
    {
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.GetComponent<BonusScore>().Destroy();
        }
    }
}