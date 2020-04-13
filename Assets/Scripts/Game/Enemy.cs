using UnityEngine;

public class Enemy : MonoBehaviour
{
    private GameManager gm;

    private bool isReady = false;
    private Rigidbody2D rb;
    private GameObject Player;
    public float movementSpeed = 40.0f;

    public GameObject bulletSpawner;
    public GameObject EnemyBulletPrefab;
    public float bulletSpeed = 30.0f;

    private void Awake()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();

        rb = this.gameObject.GetComponent<Rigidbody2D>();
        Player = GameObject.FindGameObjectWithTag("Player");
        
        foreach(Transform t in this.transform)
        {
            if(t.name == "BulletSpawner")
                this.bulletSpawner = t.gameObject;
        }
    }

    private void Start()
    {
        this.Prepare();
    }

    private void Update()
    {
        this.Shoot();
    }

    private Vector3 target;
    private void Prepare()
    {
        target = Player.transform.position - this.transform.position;
        float angle = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg;
        this.transform.rotation = Quaternion.AngleAxis(angle - 90.0f, Vector3.forward);
    }

    public void Move()
    {
        this.isReady = true;
        this.rb.AddForce(target.normalized * movementSpeed);
    }

    private float timer = 0.0f;
    private void Shoot()
    {
        if(this.isReady)
        {
            this.timer += Time.deltaTime;

            if(this.timer >= 0.7f)
            {
                GameObject bullet = Instantiate(EnemyBulletPrefab, bulletSpawner.transform.position, this.gameObject.transform.rotation);
                bullet.transform.SetParent(gm.LevelObjects);
                bullet.GetComponent<Rigidbody2D>().velocity = transform.up * bulletSpeed;
                this.timer = 0.0f;
            }
        }
    }

    public void Die()
    {
        this.timer = -1.4f;
        this.isReady = false;
        this.gameObject.GetComponent<Animator>().SetBool("isDied", true);
        this.rb.constraints = RigidbodyConstraints2D.FreezeAll;
        this.gameObject.GetComponent<PolygonCollider2D>().enabled = false;
        gm.enemyCount--;
    }

    public void Destroy()
    {
        Destroy(this.gameObject);
    }
}
