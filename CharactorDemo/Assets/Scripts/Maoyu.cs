using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maoyu : MonoBehaviour
{
    private Animator Anim;
    private Rigidbody2D rb;
    private Collider2D Coll;
    public LayerMask ground;
    public Transform leftpoint, rightpoint;
    public float Speed, jumpForce;
    private float leftx, rightx;
    private bool Faceleft = true;
    // Start is called before the first frame update
    void Start()
    {
        Anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        Coll = GetComponent<Collider2D>();

        //transform.DetachChildren();
        leftx = leftpoint.position.x;
        rightx = rightpoint.position.x;
        Destroy(leftpoint.gameObject);
        Destroy(rightpoint.gameObject);

    }

    // Update is called once per frame
    void Update()
    {
        SwitchAnim();
    }
    void Movement()
    {
        if (Faceleft == true)
        {
            if (Coll.IsTouchingLayers(ground))
            {
                Anim.SetBool("jumping", true);
                if (transform.position.x < leftx)
                {

                    transform.localScale = new Vector3(-1, 1, 1);

                    Faceleft = false;
                    rb.velocity = new Vector2(Speed, jumpForce);
                    return;
                }
                transform.localScale = new Vector3(1, 1, 1);
                rb.velocity = new Vector2(-Speed, jumpForce);
            }
        }
        else
        {
            if (Coll.IsTouchingLayers(ground))
            {
                Anim.SetBool("jumping", true);




                if (transform.position.x > rightx)
                {
                    transform.localScale = new Vector3(1, 1, 1);

                    Faceleft = true;
                    rb.velocity = new Vector2(-Speed, jumpForce);
                    return;
                }
                transform.localScale = new Vector3(-1, 1, 1);
                rb.velocity = new Vector2(Speed, jumpForce);
            }
        }
    }
    void SwitchAnim()
    {
        if (Anim.GetBool("jumping"))
        {
            if (rb.velocity.y < 0.1)
            {
                Anim.SetBool("jumping", false);
                Anim.SetBool("falling", true);
                //Debug.Log("1");
            }
        }
        if (Coll.IsTouchingLayers(ground) && Anim.GetBool("falling"))
        {
            Anim.SetBool("falling", false);
        }
    }
}
