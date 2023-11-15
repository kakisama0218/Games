using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mojiex;

public class DialogTriggerEnter : MonoBehaviour
{
    //private Animator anim;
    public Image Dialog;
    private Animator dialogAnim;
    // Start is called before the first frame update
    void Start()
    {
        //anim = GetComponent<Animator>();
        dialogAnim = Dialog.GetComponent<Animator>();
        if(DataStatic.Inst.dataModel.isGot(21))
        {
            this.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Dialog.gameObject.SetActive(true);
            dialogAnim.SetBool("Quit", false);
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
      
            //anim.SetBool("Open", false);
            dialogAnim.SetBool("Quit", true);
        
    }
    private void quitdialog()
    {
        Dialog.gameObject.SetActive(false);
    }
}
