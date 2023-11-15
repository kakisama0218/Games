using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mojiex;

public class Coin : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rg;
    private Collider2D coll;
    public LayerMask ground;

    private bool isGot;

    //public Text CoinUI;
    //private bool isGot=false;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        anim.SetBool("isGot", false);
        rg = GetComponent<Rigidbody2D>();

        float angel = Random.Range(-30f, 30f);
        rg.velocity = Quaternion.AngleAxis(angel,Vector3.forward)* Vector2.up * 10;

        isGot = false;
    }
    void Awake()
    {
        //rg.velocity = Vector3.up * 10;
        //rg.velocity = Vector2.up * 10;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (coll.IsTouchingLayers(ground) == true)
        {
            //Debug.Log("coin");
            if (other.gameObject.CompareTag("Player") && other.GetType().ToString()=="UnityEngine.BoxCollider2D")
            {
                if(!isGot)
                {
                    DataStatic.Inst.dataModel.SetCoinNum(DataStatic.Inst.dataModel.GetCoinNum()+1);
                    isGot = true;
                    //Debug.Log("coin");
                }               
                
                anim.SetBool("isGot", true);
            }
        }
       
    }

    void CoinGot()
    {

    }

    void Death()
    {
        Destroy(this.gameObject);
    }
}
