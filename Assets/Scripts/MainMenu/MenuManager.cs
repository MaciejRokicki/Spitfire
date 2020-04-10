using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    private void Awake()
    {
        GameObject.Find("TransitionPanel").GetComponent<Image>().material.SetFloat("_Scale", 15.0f);
    }

    private void Start()
    {
        GameObject.Find("TransitionPanel").GetComponent<Image>().material.SetFloat("_Fade", 0.0f);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(GameObject.Find("TransitionPanel").GetComponent<TransitionEffect>().ShowEffect(changeScene: true));
        }
    }
}