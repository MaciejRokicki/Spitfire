using UnityEngine;
using TMPro;

public class MenuManager : MonoBehaviour
{
    private AppManager appManager;

    public GameObject transitionEffect, background;
    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI pressKeyToPlayText;

    private void Awake()
    {
        appManager = GameObject.Find("AppManager").GetComponent<AppManager>();
    }

    private void Start()
    {
        switch(appManager.deviceType)
        {
            case DeviceType.Desktop:
                pressKeyToPlayText.SetText("Wciśnij spację, aby rozpocząć");
                break;

            case DeviceType.Handheld:
                pressKeyToPlayText.SetText("Dotknij ekranu, aby rozpocząć");
                break;
        }

        if(PlayerPrefs.HasKey("HighScore"))
        {
            highScoreText.text += PlayerPrefs.GetInt("HighScore");
        }
        else
        {
            highScoreText.text += 0;
        }

    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) || Input.touchCount > 0)
        {
            transitionEffect.GetComponent<Animator>().SetTrigger("Show");
        }
    }
}