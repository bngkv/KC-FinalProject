using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_script : MonoBehaviour
{
    public float walkSpeed, range, timeBTWShots, ShootSpeed;
    private float distoplayer;

    [HideInInspector]
    public bool mustPatrol;
    private bool mustTurn, canShoot;

    public Rigidbody2D rb;
    public Transform groundCheckPos;
    public LayerMask groundLayer;
    public Collider2D bodyCollider;
    public Transform player, ShootPos;
    public GameObject bullet;
    // Start is called before the first frame update
    void Start()
    {
        mustPatrol = true;
        canShoot = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (mustPatrol)
        {
            Patrol();

        }
        distoplayer = Vector2.Distance(transform.position, player.position);
        
        if(distoplayer <= range)
        {
            if (player.position.x > transform.position.x && transform.localScale.x < 0
                || player.position.x < transform.position.x && transform.localScale.x > 0) 
            {
                Flip();
            }


            mustPatrol=false;
            rb.velocity = Vector2.zero;
           
            if(canShoot)
            StartCoroutine(Shoot());

       
        }
        else
        {
            mustPatrol=(true);
        }
    }

    private void FixedUpdate()
    {
        if(mustTurn)
        {
            mustTurn = !Physics2D.OverlapCircle(groundCheckPos.position, 0.1f, groundLayer);
        }
    }



    void Patrol()
    {   if(mustTurn || bodyCollider .IsTouchingLayers(groundLayer))
        {
            Flip();

        }
        
        rb.velocity = new Vector2(walkSpeed * Time.fixedDeltaTime, rb.velocity.y);
    }
    private void FixedUpdete()
    {
        if (mustPatrol)
        {
            mustTurn = !Physics2D.OverlapCircle(groundCheckPos.position, 0.1f, groundLayer);
        }

    }






    void Flip()
    {
        mustPatrol = false;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        walkSpeed *= -1;
        mustPatrol = true;
    }
    

    IEnumerator Shoot()
    { canShoot = false;
        yield return new WaitForSeconds(timeBTWShots);
        GameObject newBullet = Instantiate(bullet, ShootPos.position, Quaternion.identity);
        newBullet.GetComponent<Rigidbody>().velocity = new Vector2(ShootSpeed * walkSpeed * Time.fixedDeltaTime, 0f);
        Debug.Log("Shoot");
        canShoot = true;
    }  



}
  





















    
        
    






 
