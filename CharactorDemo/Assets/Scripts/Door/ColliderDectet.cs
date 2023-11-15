using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ColliderDectet : MonoBehaviour
{
    [SerializeField]
    private string _colliderScripts;
    [SerializeField]
    private UnityEvent _collisionEntered;
    [SerializeField]
    private UnityEvent _collisionExit;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent(_colliderScripts))
        {
            _collisionEntered.Invoke();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent(_colliderScripts))
        {
            _collisionExit.Invoke();
        }
    }
}
