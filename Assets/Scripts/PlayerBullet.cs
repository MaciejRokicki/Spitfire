using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.tag == "Enemy")
        {
            coll.GetComponent<Enemy>().Die();
            GameObject.Find("GameManager").GetComponent<GameManager>().IncreaseScore(100);
        }

        if(coll.tag == "Wall")
        {
            Destroy(this.gameObject);
        }
    }
}
