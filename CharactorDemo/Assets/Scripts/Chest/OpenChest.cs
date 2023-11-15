using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mojiex;

public class OpenChest : MonoBehaviour
{
    public GameObject chest;
    public GameObject CoinPoint;
    private Animator anim;
    public GameObject Item;
    private Rigidbody2D rb;
    public int ItemNum;
    private float TimeCounter = 0.15f;

    private bool isOpened=false;

    //public int ChestID;
    public int ItemID;
    // Start is called before the first frame update
    void Start()
    {
        if (DataStatic.Inst.dataModel.isGot(ItemID))
        {
            isOpened = true;
        }
        anim = chest.GetComponent<Animator>();
        rb = Item.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            if(!isOpened)
            {
                DataStatic.Inst.dataModel.addItem(ItemID);
                anim.SetBool("Open", true);
                int i;
                for (i = 1; i < ItemNum + 1; i++)
                {
                    Invoke("GetCoin", TimeCounter);
                }
                isOpened = true;
            }
        }
    }
    void GetCoin()
    {
        Instantiate(Item, CoinPoint.transform.position, Quaternion.identity);
        //rb.AddForce(Item.transform.up * 10, ForceMode2D.Force);
        //rb.velocity = new Vector2(rb.velocity.x, 15);
    }
}
