using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mojiex;

public class GameRestart : MonoBehaviour
{
    public GameObject DataSaveNum;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void RestartGame()
    {
        //DataStatic.Inst.SetDataID(1);
        //UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        //DataStatic.Inst.dataModel.removeItem(21);
        DataStatic.Inst.SetDataID(DataSaveNum.GetComponent<SaveDataNum>().DataNum);
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);

    }
}
