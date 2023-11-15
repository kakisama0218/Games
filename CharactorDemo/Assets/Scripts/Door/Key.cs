using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mojiex;

public class Key : MonoBehaviour
{
    private Animator anim;
    public Transform UIPanel;
    public ParticleSystem ParticalUp;
    public int ItemID;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();  
        if(DataStatic.Inst.dataModel.isGot(ItemID))
        {
            gameObject.SetActive(false);
        }
    }
    [ContextMenu(itemName: "isGot")]
    public void isGot()
    {
        DataStatic.Inst.dataModel.addItem(ItemID);
        anim.SetBool("isGot", true);
    }
    public void onUI()
    {
        UIPanel.gameObject.SetActive(true);
    }
    void Death()
    {
        Destroy(this.gameObject);
    }
    public void Partical()
    {
        ParticalUp.Stop();
    }
}
