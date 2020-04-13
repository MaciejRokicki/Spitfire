using UnityEngine;
using UnityEngine.UI;

public class BackgroundColorChanger : MonoBehaviour
{
    private Image image;

    private void Awake()
    {
        image = GameObject.Find("Background").GetComponent<Image>();
    }

    public void ChangeColor()
    {
        image.color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
    }
}
