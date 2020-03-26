using UnityEngine;

public class PlayerControllerTest : MonoBehaviour
{
    public Rigidbody2D rb;
    public Camera mainCamera;
    public float movementSpeed = 8.0f;
    //internal bool canShoot = true;
    public GameObject BulletSpawner;
    public GameObject BulletPrefab;
    public float bulletSpeed = 20.0f;

    private void Awake()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
    }

    void Start()
    {

    }

    private float time = 0.0f;


    void Update()
    {
        time += Time.deltaTime;

        transform.Translate(new Vector2(0.0f, movementSpeed * Time.deltaTime));
        rb.MoveRotation(rb.rotation - (Input.GetAxisRaw("Horizontal") * 4));

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
        Debug.Log(col.gameObject.name + " : " + gameObject.name + " : " + Time.time);
    }
}
