using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SceneLoader : MonoBehaviour
{
    public PolygonCollider2D coll;
    public CinemachineVirtualCamera cam1,cam2;
    public Animator Transistion;
    public GameObject enemyPosition;

    public GameObject Enemy;
    public bool inArea;
    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<PolygonCollider2D>();
        //cam1.gameObject.SetActive(true);
        inArea = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //Debug.Log("1");
            Transistion.SetBool("On",true);
            cam1.gameObject.SetActive(true);
            cam2.gameObject.SetActive(false);
           /* if(!inArea)
            {
                Instantiate(Enemy, enemyPosition.transform.position, Quaternion.identity);
                inArea = true;
            }*/
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag=="Player")
        {
            Transistion.SetBool("On", false);
            Invoke("tranAnim", 0.5f);
            /*if(inArea)
            {
                Destroy(Enemy.gameObject);
                //inArea = false;
            }*/
            //Transistion.SetTrigger("Exit");
            //tranAnim();
        }

    }
    private void tranAnim()
    {
        Transistion.SetBool("On", true);
      
    }
   
}
