using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveDataNum : MonoBehaviour
{
    public int DataNum;
    // Start is called before the first frame update
    void Start()
    {
        GameObject.DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
