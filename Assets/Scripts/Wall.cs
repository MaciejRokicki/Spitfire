using UnityEngine;

public class Wall : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.tag == "Enemy")
        {
            coll.GetComponent<Enemy>().Die();
        }

        if(coll.tag == "Player")
        {
            coll.GetComponent<PlayerController>().Die();
        }
    }
}