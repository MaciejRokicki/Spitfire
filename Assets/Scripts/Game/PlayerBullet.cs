using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    private GameManager gm;

    private void Awake()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.tag == "Enemy")
        {
            coll.GetComponent<Enemy>().Die();
            gm.SpawnBonusScore();
            gm.SpawnAddedScore(new Vector3(coll.transform.position.x, coll.transform.position.y + 1.5f, coll.transform.position.z), 100);
            gm.IncreaseScore(100);
        }

        if(coll.tag == "BonusScore")
        {
            coll.GetComponent<BonusScore>().Die();
            gm.SpawnAddedScore(new Vector3(coll.transform.position.x, coll.transform.position.y + 1.5f, coll.transform.position.z), 500);
            gm.IncreaseScore(500);
        }

        if(coll.tag == "Wall")
        {
            Destroy(this.gameObject);
        }
    }
}
