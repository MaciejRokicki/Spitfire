using UnityEngine;

public class AppManager : MonoBehaviour
{

    public DeviceType deviceType;

    private void Awake()
    {
        if (FindObjectsOfType<MenuManager>().Length != 1)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
        }

        deviceType = SystemInfo.deviceType;
    }

    private void Start()
    {
        Screen.SetResolution(1280, 720, false, 60);
    }
}
