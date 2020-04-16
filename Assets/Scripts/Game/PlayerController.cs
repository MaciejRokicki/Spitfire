using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private AppManager appManager;

    private GameManager gameManager;
    public Rigidbody2D rb;
    public Camera mainCamera;
    private bool isDead = false;
    public float movementSpeed = 40.0f;
    public GameObject BulletSpawner;
    public GameObject BulletPrefab;
    public float bulletSpeed = 30.0f;

    private void Awake()
    {
        appManager = GameObject.Find("AppManager").GetComponent<AppManager>();

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Update()
    {
        Shoot();
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.tag == "Enemy")
        {
            this.Die();
        }
    }

    private void Move()
    {
        if(!isDead)
        {
            Quaternion rot = default;

            if (appManager.deviceType == DeviceType.Desktop)
            {
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                rot = Quaternion.LookRotation(transform.position - mousePosition, Vector3.forward);
            }
            if(appManager.deviceType == DeviceType.Handheld)
            {
                Joystick joystick = GameObject.Find("Joystick").GetComponent<Joystick>();
                Vector3 direction = joystick.direction;
                Vector3 forward = transform.position.normalized - direction.normalized;

                if(forward != Vector3.zero)
                    rot = Quaternion.LookRotation(forward, Vector3.forward);
            }

            transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);
            rb.angularVelocity = 0;
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.fixedDeltaTime * 6.0f);

            rb.AddForce(gameObject.transform.up * movementSpeed);
        }
    }

    private float time = 0.0f;

    private void Shoot()
    {
        if(!isDead)
        {
            time += Time.deltaTime;

            if(time >= 0.5f)
            {
                GameObject bullet = Instantiate(BulletPrefab, BulletSpawner.transform.position, this.gameObject.transform.rotation);
                bullet.transform.SetParent(gameManager.LevelObjects);
                bullet.GetComponent<Rigidbody2D>().velocity = transform.up * bulletSpeed;
                time = 0.0f;
            }
        }
    }

    public void Die()
    {
        this.isDead = true;
        GameObject.Find("GameManager").GetComponent<GameManager>().player = null;
        this.gameObject.GetComponent<Animator>().SetBool("isDied", true);
        this.rb.constraints = RigidbodyConstraints2D.FreezeAll;
        gameManager.player = null;
    }
}
