using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BloodStripEffect : MonoBehaviour
{
    public Image _unMaskImage;
    public Image _MaskedImage;
    public Animator _animator;
    public void InitBloodStripEffect(float _cur,float _pre)
    {
        _unMaskImage.fillAmount = _cur;
        _MaskedImage.fillAmount = _pre;
        _animator.SetBool("Show", true);
        StartCoroutine(ReallyMoveEffect());
    }
    IEnumerator ReallyMoveEffect()
    {
        float a = 400;
        yield return new WaitForSeconds(0.1f);
        while(true)
        {
            a -= 10;
            MoveEffect(a);
            yield return new WaitForFixedUpdate();
        }
    }

    void MoveEffect(float a)
    {
        transform.position += (Vector3.up * a * Time.deltaTime);
    }

    public void DestroyEffect()
    {
        Destroy(this.gameObject);
    }
}
