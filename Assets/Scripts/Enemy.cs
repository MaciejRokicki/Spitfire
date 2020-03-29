using UnityEngine;

public class Enemy : MonoBehaviour
{

    private Rigidbody2D rb;
    private ParticleSystem particle;
    private GameObject Player;
    public float movementSpeed = 40.0f;

    public GameObject BulletSpawner;
    public GameObject EnemyBulletPrefab;
    public float bulletSpeed = 30.0f;

    void Awake()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        particle = this.gameObject.GetComponent<ParticleSystem>();
        Player = GameObject.Find("Player");
        
        foreach(Transform t in this.transform)
        {
            if(t.name == "BulletSpawner")
                BulletSpawner = t.gameObject;
        }
    }

    void Start()
    {
        Vector3 target = Player.transform.position - this.transform.position;

        float angle = Mathf.Atan2(target.y, target.x) *Mathf.Rad2Deg;
        this.transform.rotation = Quaternion.AngleAxis(angle - 90.0f, Vector3.forward);

        this.rb.AddForce(target.normalized * movementSpeed);
    }

    private float timer = 0.0f;

    void Update()
    {
        timer += Time.deltaTime;

        if(timer >= 0.7f)
        {
            timer = 0.0f;
            Shoot();
        }
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(EnemyBulletPrefab, BulletSpawner.transform.position, this.gameObject.transform.rotation);
        bullet.GetComponent<Rigidbody2D>().velocity = transform.up * bulletSpeed;
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.tag == "PlayerBullet" || coll.tag == "Wall")
        {
            this.Die();
        }
    }

    private void Die()
    {
        particle.Play();
        this.rb.constraints = RigidbodyConstraints2D.FreezeAll;
        this.gameObject.GetComponent<Enemy>().enabled = false;
        this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        this.gameObject.GetComponent<TrailRenderer>().enabled = false;
        this.gameObject.GetComponent<PolygonCollider2D>().enabled = false;
    }
}
