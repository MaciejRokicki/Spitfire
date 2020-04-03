using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.tag == "Enemy")
        {
            coll.GetComponent<Enemy>().Die();
            GameObject.Find("GameManager").GetComponent<GameManager>().SpawnAddedScore(new Vector3(coll.transform.position.x, coll.transform.position.y + 1.5f, coll.transform.position.z), 100);
            GameObject.Find("GameManager").GetComponent<GameManager>().IncreaseScore(100);
        }

        if(coll.tag == "Wall")
        {
            Destroy(this.gameObject);
        }
    }
}
