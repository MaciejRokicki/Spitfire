using UnityEngine;

public class MenuManager : MonoBehaviour
{

    private void Awake()
    {

    }

    private void Start()
    {

    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) || Input.touchCount > 0)
        {
            GameObject.Find("TransitionEffect").GetComponent<Animator>().SetTrigger("Show");
        }
    }
}