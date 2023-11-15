using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Mojiex;

public class Heal : MonoBehaviour
{
    public GameObject player;
    public GameObject UIPanel;
    public int ID;
    public Vector3 offset;

    public GameObject enemyPoint;
    public GameObject enemy;
    public GameObject EnemyGroup;
    //public UnityEvent<Charactor> OnHealChange;
    // Start is called before the first frame update
    void Start()
    {
        /* if (DataStatic.Inst.dataModel.GetBornFire() == ID)
         {
             FindObjectOfType<FinalMove>().transform.position = transform.position + offset;
         }*/
        enemy = GameObject.FindGameObjectWithTag("EnemyGroup");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            player.GetComponent<Charactor>()?.GetHeal();
           
            UIPanel.gameObject.SetActive(true);

            enemy = GameObject.FindGameObjectWithTag("EnemyGroup");
            Destroy(enemy.gameObject);
            Instantiate(EnemyGroup, enemyPoint.transform.position, Quaternion.identity);

            DataStatic.Inst.dataModel.SetBornFire(ID);
            DataStatic.Inst.Save();

           


        }
    }
}
