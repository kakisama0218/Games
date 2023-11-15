using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BloodStrip : MonoBehaviour
{

    public Image _BloodStripImage;
    float MaxHP = 100;
    float CurHP = 100;

    public GameObject _bloodEffectObj;
    

    public void GetDamage(float damage)
    {
        CurHP -= damage;
        float _preFillAmount = _BloodStripImage.fillAmount;      
        float _curFillAmount = (float)CurHP / (float)MaxHP;
        _BloodStripImage.fillAmount = _curFillAmount;
        GameObject obj = Instantiate(_bloodEffectObj, this.transform.parent);
        obj.transform.SetAsFirstSibling();
        obj.GetComponent<BloodStripEffect>().InitBloodStripEffect(_curFillAmount, _preFillAmount);
    }
    public void BornFireHeal(float Hp)
    {
        CurHP = Hp;
        float _preFillAmount = _BloodStripImage.fillAmount;
        float _curFillAmount = (float)CurHP / (float)MaxHP;
        _BloodStripImage.fillAmount = _curFillAmount;
      

    }
   
    private void Update()
    {
       if(CurHP>MaxHP)
        {
            CurHP = MaxHP;
        }
      /*  if (Input.GetKeyDown(KeyCode.Z))
        {
            GetDamage(10);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            GetDamage(30);
        }*/
    }
}
