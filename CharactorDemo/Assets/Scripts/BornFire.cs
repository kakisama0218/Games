using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mojiex;

public class BornFire : MonoBehaviour
{
    //private Animator anim;
    public Image Dialog;
    private Animator dialogAnim;
    public int ID;
    public Vector3 offset;
    //private bool Opened;

    private void Start()
    {
        //anim = GetComponent<Animator>();
        dialogAnim = Dialog.GetComponent<Animator>();
        //Debug.Log(DataStatic.Inst.dataModel.GetBornFire() == ID);
        if(DataStatic.Inst.dataModel.GetBornFire()==ID)
        {
            FindObjectOfType<FinalMove>().transform.position = transform.position+offset;
        }
        //Opened = false;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.tag == "Player")
        {
            Dialog.gameObject.SetActive(true);
            dialogAnim.SetBool("Quit", false);
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {

        //anim.SetBool("Open", false);
        dialogAnim.SetBool("Quit", true);

    }
    public void quitdialog()
    {
        Dialog.gameObject.SetActive(false);
    }
}
