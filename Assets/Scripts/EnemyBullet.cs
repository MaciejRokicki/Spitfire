using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.tag == "Wall")
        {
            Destroy(this.gameObject);
        }

        if(coll.tag == "Player")
        {
            coll.GetComponent<PlayerController>().Die();
        }
    }
}