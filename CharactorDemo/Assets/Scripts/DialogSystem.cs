using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mojiex;

public class DialogSystem : MonoBehaviour
{
    [Header("UI设置")]
    public Text textLabel;
    public Image faceImage;

    [Header("字幕设置")]
    public TextAsset textFile;
    public int index;
    public float textSpeed;

    [Header("头像置换")]
    public Sprite head01, head02;

    [Header("其他")]
    bool textFinished;

    public Animator anim;

    public int ItemID;

    List<string> textList = new List<string>();

    // Start is called before the first frame update
    private void Start()
    {
        if (DataStatic.Inst.dataModel.isGot(ItemID))
        {
            gameObject.SetActive(false);
        }
    }

    void Awake()
    {
        GetTextFormFile(textFile);
    }

    // Update is called once per frame
    private void OnEnable()
    {
        textFinished = true;
        StartCoroutine(SetTextUI());
    }
    private void Update()
    {
        //Debug.Log("11");
        if(Input.GetKeyDown(KeyCode.E)&&index==textList.Count)
        {
            //Debug.Log("1");
            anim.SetTrigger("quit");
            index = 0;
            return;
        }
        if(Input.GetKeyDown(KeyCode.E)&&textFinished)
        {
            StartCoroutine(SetTextUI());
        }
    }
    void GetTextFormFile(TextAsset file)
    {
        textList.Clear();
        index = 0;

        var lineData = file.text.Split('\n');

        foreach(var line in lineData)
        {
            textList.Add(line);
        }
    }

    void QuitDialog()
    {
        DataStatic.Inst.dataModel.addItem(ItemID);
        gameObject.SetActive(false);

    }

    IEnumerator SetTextUI()
    {
        textFinished = false;
        textLabel.text = "";

        switch(textList[index].Trim().ToString())
        {
            case "A":
                faceImage.sprite = head01;
                index++;
                break;
            case "B":
                faceImage.sprite = head02;
                index++;
                break;
        }
        for(int i=0;i<textList[index].Length;i++)
        {
            textLabel.text += textList[index][i];
            yield return new WaitForSeconds(textSpeed);
        }
        textFinished = true;
        index++;
    }

    
}
