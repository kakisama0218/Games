using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mojiex;

public class Door : MonoBehaviour
{
    public GameObject Trigger;
    public GameObject Key;

    public GameObject Tips;

    private Animator anim;
    private Animator TipsAnim;
    //private bool isOpened=>DataStatic.Inst.dataModel.isGot(Key.GetComponent<Key>().ItemID);
    private bool isOpened => DataStatic.Inst.dataModel.isGot(2);

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        TipsAnim = Tips.GetComponent<Animator>();
        //isOpened = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag=="Player")
        {
          
            if (isOpened)
            {
                anim.SetBool("Open", true);
                Trigger.gameObject.SetActive(false);
               
            }
            else
            {
                Tips.gameObject.SetActive(true);
                
            }
        }
             
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        anim.SetBool("Open", false);
        Trigger.gameObject.SetActive(true);
        TipsAnim.SetTrigger("TipsOff");
        Invoke("TipsOff", 0.3f);
        //Tips.gameObject.SetActive(false);
    }
    [ContextMenu(itemName:"GetKey")]
    public void GetKey()
    {
        //isOpened = true;
        //Debug.Log(this.gameObject.ToString());
    }
    public void TipsOff()
    {
        Tips.gameObject.SetActive(false);
    }
}
