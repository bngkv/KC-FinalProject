using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deeek : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rb;
    public float speed;
    public float jump;
    bool canJump;
    SpriteRenderer sprite;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        
    }

    void Update()
    {

        Vector2 temp = rb.velocity;

        //flips the character:
        if (Input.GetAxis("Horizontal") > 0)
            sprite.flipX = false;
        else if (Input.GetAxis("Horizontal") < 0)
            sprite.flipX = true;


        //jumping:
        if (canJump && Input.GetKeyDown(KeyCode.Space))
        {

            temp.y = jump;
            canJump = false;

        }
        
        //running:
        temp.x = Input.GetAxis("Horizontal") * speed;
        rb.velocity = temp;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Ground")
        {

        }
    }
}