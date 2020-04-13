using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGameScene : StateMachineBehaviour
{
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        SceneManager.LoadScene("Game");
    }
}
