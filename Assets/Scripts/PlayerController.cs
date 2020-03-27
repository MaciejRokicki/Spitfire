using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public Camera mainCamera;
    public float movementSpeed = 40.0f;
    //internal bool canShoot = true;
    public GameObject BulletSpawner;
    public GameObject BulletPrefab;
    public float bulletSpeed = 30.0f;

    private void Awake()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
    }

    void Start()
    {

    }

    private float time = 0.0f;

    void FixedUpdate()
    {
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Quaternion rot = Quaternion.LookRotation(transform.position - mousePosition, Vector3.forward);

        transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);
        rb.angularVelocity = 0;
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.fixedDeltaTime * 3.0f);

        rb.AddForce(gameObject.transform.up * movementSpeed);
    }

    void Update()
    {
        time += Time.deltaTime;

        if(/*canShoot &&*/ time >= 0.5f)
        {
            Shoot();
            time = 0.0f;
        }
    }


    private void Shoot()
    {
        GameObject bullet = Instantiate(BulletPrefab, BulletSpawner.transform.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody>().velocity = transform.up * bulletSpeed;
    }

    void OnTriggerEnter2D(Collider2D col)
    {

    }
}
