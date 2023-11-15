using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chest : MonoBehaviour
{
    private Animator anim;
    public Image Dialog;
    private Animator dialogAnim;
    //private bool Opened;

    private void Start()
    {
        anim = GetComponent<Animator>();
        dialogAnim = Dialog.GetComponent<Animator>();
        //Opened = false;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        
        if (collision.tag=="Player")
        {
            Dialog.gameObject.SetActive(true);
            dialogAnim.SetBool("Quit", false);
           
        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {

        anim.SetBool("Open", false);
        dialogAnim.SetBool("Quit", true);
        
    }
    private void quitdialog()
    {
        Dialog.gameObject.SetActive(false);
    }
}
