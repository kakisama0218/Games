using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Mojiex;

public class LoadGame : MonoBehaviour
{
    public int ButtonID;
    public Text DataText;
    public GameObject SceneLoader;
    private Animator anim;
    private Button btn;

    public GameObject loader;
    public GameObject DataSaveNum;
    public void Start()
    {
        btn = GetComponent<Button>();

        anim = SceneLoader.gameObject.GetComponent<Animator>();
        if (DataStatic.Inst.DataListModel.GetData(ButtonID) != null)
        {
            DataText.text = "Data" + (ButtonID+1).ToString();
        }
        else
            DataText.text = "New Game";

       /* if (DataStatic.Inst.DataListModel.GetData(ButtonID) == null&&ButtonID!=0)
        {
            btn.interactable = false;
        }*/


    }
    public void GetBtnID()
    {
        if (DataStatic.Inst.DataListModel.GetData(ButtonID) != null)
        {
            DataStatic.Inst.SetDataID(ButtonID);
            DataSaveNum.GetComponent<SaveDataNum>().DataNum=ButtonID;
        }
       else
        {
            DataStatic.Inst.DataListModel.NewData();
            DataStatic.Inst.SetDataID(ButtonID);
        }
       
    }
    public void DeleteData()
    {
        DataStatic.Inst.DataListModel.RemoveData(ButtonID);
        if (DataStatic.Inst.DataListModel.GetData(ButtonID) != null)
        {
            DataText.text = "Data" + (ButtonID + 1).ToString();
        }
        else
            DataText.text = "New Game";

    }
    public void GameStart()
    {
        anim.SetBool("switch", true);
        loader.SetActive(true);
        Invoke("GameLoad", 1);

    }
    public void GameLoad()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
    }
}
