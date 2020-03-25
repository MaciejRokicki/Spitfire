using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    public float movementSpeed = 2.0f;

    private void Awake()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
    }

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            this.rb.velocity = new Vector2(0.0f, movementSpeed);
        }

        if (Input.GetKey(KeyCode.A))
        {
            this.rb.velocity = new Vector2(-movementSpeed, this.rb.velocity.y);
        }

        if (Input.GetKey(KeyCode.S))
        {
            this.rb.velocity = new Vector2(0.0f, -movementSpeed);
        }

        if(Input.GetKey(KeyCode.D))
        {
            this.rb.velocity = new Vector2(movementSpeed, this.rb.velocity.y);
        }
    }
}
