using UnityEngine;

public class PlayerDieStateMachine : StateMachineBehaviour
{
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameObject.Find("TransitionEffect").GetComponent<Animator>().SetTrigger("Show");
        GameObject.Find("GameManager").GetComponent<GameManager>().NewGame(); 
    }
}
