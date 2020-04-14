using UnityEngine;

public class BonusScore : MonoBehaviour
{
    private GameManager gm;

    private Rigidbody2D rb;
    private GameObject Player;
    public float movementSpeed = 40.0f;

    private void Awake()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();

        rb = this.gameObject.GetComponent<Rigidbody2D>();
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Start()
    {
        this.Prepare();
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
        this.rb.AddForce(target.normalized * movementSpeed);
    }

    public void Die()
    {
        this.gameObject.GetComponent<Animator>().SetBool("isDied", true);
        this.rb.constraints = RigidbodyConstraints2D.FreezeAll;
        this.gameObject.GetComponent<CircleCollider2D>().enabled = false;
        gm.enemyCount--;
    }

    public void Destroy()
    {
        Destroy(this.gameObject);
    }

}
