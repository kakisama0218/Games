using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public float speed;
    private Vector2 direction;
    private bool isHit;
    private bool isDead=false;
    private AnimatorStateInfo info;
    private Animator anim;
    private Rigidbody2D rb;
    public Transform hit;
    private Animator hitAnim;
    private Animator hitFx;
    public GameObject fx;

    // Start is called before the first frame update
    void Start()
    {
        anim = transform.GetComponent<Animator>();
        rb = transform.GetComponent<Rigidbody2D>();
        hitAnim = hit.GetComponent<Animator>();
        hitFx = fx.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        info = anim.GetCurrentAnimatorStateInfo(0); 
        if(isHit)
        {
            rb.velocity = -direction * speed;
            if (info.normalizedTime >= .6f)
                isHit = false;
        }
    }
    public void GetHit(Vector2 direction)
    {

        if(!isDead)
        {
            transform.localScale = new Vector3(-direction.x, 1, 1);
            isHit = true;
            this.direction = direction;


            anim.SetTrigger("Hit");
            hitAnim.SetTrigger("Hit");
            float angel = Random.Range(-180f, 180f);
            //Debug.Log(angel);
            hitFx.transform.Rotate(0,0,angel,Space.World);
            hitFx.SetTrigger("HitFX");

        }
        //hitfx();
    }

    public void OnDie()
    {
        if (!isDead)
        {
            isDead = true;
            anim.SetTrigger("Dead");
        }
        
    }
    public void Dead()
    {
        Destroy(this.gameObject);
    }
    void hitfx()
    {
        Instantiate(fx, transform.position, Quaternion.identity);
    }
}
