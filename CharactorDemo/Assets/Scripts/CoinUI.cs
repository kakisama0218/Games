using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mojiex;

public class CoinUI : MonoBehaviour
{
    public Text CoinText;
    public int CoinNum;

    public static int CurrentCoinQuantity;

    public void Start()
    {
        CurrentCoinQuantity = CoinNum;
    }
    // Start is called before the first frame update
    public void Update()
    {  
            CoinText.text = DataStatic.Inst.dataModel.GetCoinNum().ToString().PadLeft(6,'0');
      
    }

}
