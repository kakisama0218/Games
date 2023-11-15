using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Mojiex;
using System;

public class FinalMove : MonoBehaviour
{
    public ParticleSystem dust;
    public ParticleSystem dustLeft;
    public ParticleSystem jumpDust;
    public ParticleSystem jumpdustfall;
    public ParticleSystem dashdust;
    public ParticleSystem dashdustleft;
    Rigidbody2D rb;
    Transform groundCheck;
    bool skillJumpTwice=true;
    bool jumpTwiceTemp;

    bool CanJumpTwice=>DataStatic.Inst.dataModel.isGot(1);

    private bool faceRight = true;

    private float coyoteTime = 0.2f;
    private float coyoteTimeCounter;

    public float hurtForce;
    public bool isHurt;
    private bool isDust;

    public bool isDead;
    [Header("时间参数")]
    public bool canSkill;
    public bool isSkill;
    public float Skillforce;
    public float dashtime;
    public float resumetime;
    //public float Skilltimer;
    float direction;
    public ParticleSystem DashPartical;
    public ParticleSystem DashParticalLeft;


    Animator anim;
    [Header("攻击参数")]
    private int comboStep=0;
    public float interval = 2f;
    private float timer;
    private bool isAttack=false;
    public string attackType;

    [Header("补偿速度")]
    public float lightSpeed;
    public float heavySpeed;
    [Header("打击感参数")]
    public float shaketime;
    public int lightPause;
    public float lightStrength;
    public int heavyPause;
    public float heavyStrength;

    private Cinemachine.CinemachineCollisionImpulseSource MyInpulse;

    Coroutine Skillcoroutine;

    // Start is called before the first frame update
    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        anim=GetComponent<Animator>();
        groundCheck=GameObject.Find("Player/GroundCheck").GetComponent<Transform>();
        jumpTwiceTemp=skillJumpTwice;
        //DataStatic.player.SetTwiceJump(jumpTwiceTemp);
        //EventSystem<GameEvents>.AddListener(GameEvents.SaveData, SaveData);
        //PlayerState.Inst.
        MyInpulse=GetComponent<Cinemachine.CinemachineCollisionImpulseSource>();
    }

    private void SaveData(object[] obj)
    {
        Debug.Log((string)obj[0]);
    }

    // Update is called once per frame
    void Update()
    {
        if(isSkill)
        {
            return;
        }
        if(!isHurt&&!isDead)
        {
            Move();
            Jump();
            Attack();
            //Skill();
            if (Input.GetKeyDown(KeyCode.Q) && canSkill)
            {
                //StartCoroutine(Skill());
            }
        }
        SwitchAnim();

    }
    void Move()
    {
        float facedirection =Input.GetAxisRaw("Horizontal");
        float horizontalmove = Input.GetAxis("Horizontal");
       /* if(horizontalmove!=0)
        {*/
            if(!isAttack)
            {
            if(horizontalmove!=0)
            {
                comboStep = 0;
            }
          
                rb.velocity = new Vector2(Input.GetAxis("Horizontal") * 12, rb.velocity.y);
                anim.SetFloat("running", Mathf.Abs(facedirection));
            }
            else
            {
                if(attackType=="Light")
                    rb.velocity=new Vector2(transform.localScale.x * lightSpeed, rb.velocity.y);
                if (attackType == "Heavy")
                    rb.velocity = new Vector2(transform.localScale.x * heavySpeed, rb.velocity.y);
            }
        
           
        //}
        if(faceRight==false&&horizontalmove>0)
        {
            Flip();
        }
        else if(faceRight==true&&horizontalmove<0)
        {
            Flip();
        }
       /*if(facedirection!=0)
       {
        transform.localScale=new Vector3(facedirection,1,1);
        CreateDust();
        }*/
    }
    private IEnumerator Skill()
    {
        canSkill = false;
        isSkill = true;
        isAttack = true;
        rb.gravityScale = 0;
        //Vector2 dir = new Vector2((transform.localScale.x), 0f).normalized;
       if(faceRight)
        {
         DashPartical.gameObject.SetActive(true);
        }
        if(faceRight==false)
        {
         DashParticalLeft.gameObject.SetActive(true);
        }
        rb.velocity = new Vector2(transform.localScale.x * Skillforce, 0f);
        //rb.AddForce(dir * Skillforce, ForceMode2D.Impulse);
        yield return new WaitForSeconds(dashtime);
        //DashDust();
        rb.gravityScale = 2;
        isSkill = false;
        isAttack = false;
        yield return new WaitForSeconds(resumetime);
        canSkill = true;
        DashPartical.gameObject.SetActive(false);
        DashParticalLeft.gameObject.SetActive(false);
    }


    void Flip()
    {
        if(isGround())
        {
            CreateDust();
        }       
        faceRight = !faceRight;
        Vector3 playerScale = transform.localScale;
        playerScale.x *= -1;
        transform.localScale = playerScale;
    }

    bool isGround()
    {
        return Physics2D.OverlapCircle(groundCheck.position,0.3f,LayerMask.GetMask("ground"));
    }
    void Jump()
    {
        
        if(isGround())
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }


        if(isGround()&&!jumpTwiceTemp&&CanJumpTwice)
        {
            jumpTwiceTemp=true;
        }
        if (Input.GetKeyUp(KeyCode.X))
        {
            coyoteTimeCounter = 0f;
            
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (coyoteTimeCounter>0)
            {
            rb.velocity=new Vector2(rb.velocity.x,15);
            anim.SetBool("jumping", true);
            JumpDust();
            coyoteTimeCounter = 0f;
                comboStep = 0;
            }

            else if (jumpTwiceTemp&&CanJumpTwice)
            {
                coyoteTimeCounter = 0f;
                rb.velocity = new Vector2(rb.velocity.x, 15);
                anim.SetBool("jumping", true);
                JumpDust();
                jumpTwiceTemp = false;
                comboStep = 0;

            }
        }
    }
    void SwitchAnim()
    {
        
        if(isDead)
        {
            anim.SetBool("isDead", true);
        }

        if(rb.velocity.y<0.5f&&!isGround())
        {
            anim.SetBool("falling", true);
        }
        anim.SetBool("idle", false);

        if(anim.GetBool("jumping"))
        {
            if(rb.velocity.y<0)
            {
                anim.SetBool("jumping", false);
                anim.SetBool("falling", true);
                isDust = true;
            }
        }
        else if(isGround())
        {
           anim.SetBool("falling", false);
            anim.SetBool("idle", true);
            if(isDust)
            {
                JumpDustFall();
                isDust = false;
            }
    
        }
    }

    public void PlayHurt()
    {
        anim.SetTrigger("hurt");
    }
    public void GetHurt(Transform attacker)
    {
        isHurt = true;
        rb.velocity = Vector2.zero;
        Vector2 dir =new Vector2((transform.position.x-attacker.position.x),2).normalized;
        rb.AddForce(dir * hurtForce, ForceMode2D.Impulse);

        if((transform.position.x - attacker.position.x)<0&&!faceRight)
        {
            faceRight = !faceRight;
            Vector3 playerScale = transform.localScale;
            playerScale.x *= -1;
            transform.localScale = playerScale;
        }
        else if((transform.position.x - attacker.position.x) > 0 && faceRight)
        {
            faceRight = !faceRight;
            Vector3 playerScale = transform.localScale;
            playerScale.x *= -1;
            transform.localScale = playerScale;
        }
        
    }

    public void PlayerDead(Transform attacker)
    {
        anim.SetBool("isDead", true);
        //anim.SetBool("isDead",isDead);
        if (!isDead)
        {
            rb.velocity = Vector2.zero;
            Vector2 dir = new Vector2((transform.position.x - attacker.position.x), 2).normalized;
            rb.AddForce(dir * hurtForce, ForceMode2D.Impulse);

            if ((transform.position.x - attacker.position.x) < 0 && !faceRight)
            {
                faceRight = !faceRight;
                Vector3 playerScale = transform.localScale;
                playerScale.x *= -1;
                transform.localScale = playerScale;
            }
            else if ((transform.position.x - attacker.position.x) > 0 && faceRight)
            {
                faceRight = !faceRight;
                Vector3 playerScale = transform.localScale;
                playerScale.x *= -1;
                transform.localScale = playerScale;
            }
        }
        isDead = true;

    }
    public void PlayDead()
    {
        anim.SetBool("isDead",true);
    }

    void Attack()
    {
        if(Input.GetKeyDown(KeyCode.Z)&&!isAttack)
        {
            //Debug.Log(Input.GetKeyDown(KeyCode.Z) && !isAttack);
            isAttack = true;
            attackType = "Light";
            comboStep++;
            if(comboStep>3)
                comboStep = 1;
                timer = interval;
                anim.SetTrigger("LightAttack");
            if(comboStep==3)
            {
                attackType = "Heavy";
            }
                anim.SetInteger("ComboStep", comboStep);
            
            }
            if(timer!=0)
            {
                timer -= Time.deltaTime;
                if(timer<=0)
                {
                    timer = 0;
                    comboStep = 0;
                }
            
        }
            if(Input.GetKeyDown(KeyCode.C)&&canSkill)
        {
            anim.SetTrigger("Skill");
            canSkill = false;

        }
    }

    void Dash()
    {
        StartCoroutine(Skill());
    }

    public void AttackOver()
    {
        isAttack = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if(other.tag == "Enemy"&&isAttack)
        {
            if(attackType=="Light")
            {
                MyInpulse.GenerateImpulse();
                AttackSense.Instance.HitPause(lightPause);
                AttackSense.Instance.CameraShake(shaketime, lightStrength);
            }
            if (attackType == "Heavy")
            {
                AttackSense.Instance.HitPause(heavyPause);
                AttackSense.Instance.CameraShake(shaketime, heavyStrength);
            }
            if (transform.localScale.x > 0)
            {
                other.GetComponent<enemy>().GetHit(Vector2.left);
                other.GetComponent<BossFSM>().GetHit();
            }               
            else if(transform.localScale.x < 0)
            {
                other.GetComponent<enemy>().GetHit(Vector2.right);
                other.GetComponent<BossFSM>().GetHit();
            }
               
        }
    }
    /* public void TwiceJump()
     {

         CanJumpTwice = true;
     }*/

    void CreateDust()
    {
        if(faceRight)
        {
            dustLeft.Play();
        }
        if(faceRight==false)
        {
            dust.Play();
        }
        
        
        //dust.textureSheetAnimation.flipU = -dust.textureSheetAnimation.flipU;


    }
    void JumpDust()
    {
        jumpDust.Play();
    }
    void JumpDustFall()
    {
        jumpdustfall.Play();
    }
    void DashDust()
    {
        if(isGround())
        {
            if(faceRight)
            {
                dashdust.Play();
            }
            if(faceRight==false)
            {
                dashdustleft.Play();
            }
            
        }
        
    }
}
