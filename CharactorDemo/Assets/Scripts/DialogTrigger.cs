using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    public GameObject BossDialog;
    public bool isTrigger;
    public GameObject Dialogtrigger;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            BossDialog.SetActive(true);
            Dialogtrigger.SetActive(false);
        }
        
    }
}
