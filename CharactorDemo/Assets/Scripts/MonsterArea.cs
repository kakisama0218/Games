using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterArea : MonoBehaviour
{
    public bool inArea;
    public GameObject cam;
    // Start is called before the first frame update
    void Start()
    {
        inArea = true;
        //cam=GameObject.FindObjectOfType<>
    }

    // Update is called once per frame
    void Update()
    {
        /*if(cam.activeSelf==false)
        {
            Destroy(this.gameObject);
            inArea = true;
        }*/
    }
    public void ExitArea()
    {
        Destroy(this.gameObject);
    }
}
