using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TransitionEffect : MonoBehaviour
{
    private Material material;
    private float value = 0.01f;

    private void Awake()
    {
        material = this.gameObject.GetComponent<Image>().material;
    }

    public IEnumerator ShowEffect(bool changeScene = false)
    {
        while(material.GetFloat("_Fade") < 1.0f)
        {
            material.SetFloat("_Fade", material.GetFloat("_Fade") + value);

            yield return new WaitForSeconds(value);
        }

        material.SetFloat("_Fade", 1.0f);

        if (changeScene)
            SceneManager.LoadScene("Game");
    }

    public IEnumerator HideEffect(TextMeshProUGUI counter = null)
    {
        while (material.GetFloat("_Fade") > 0.0f)
        {
            material.SetFloat("_Fade", material.GetFloat("_Fade") - value);

            yield return new WaitForSeconds(value);
        }

        material.SetFloat("_Fade", 0.0f);

        if (counter)
            counter.enabled = false;

    }
}
