using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public float speed;
    public GameObject explosionPrefab;
    new private Rigidbody2D rigidbody;
    private bool isBoom = false;
    // Start is called before the first frame update
    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }
    public void SetSpeed(Vector2 direction)
    {
        rigidbody.velocity = direction * speed;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player")||other.CompareTag("ground"))
        {
            if(!isBoom)
            {
                Instantiate(explosionPrefab, transform.position, Quaternion.identity);
                isBoom = true;
                Invoke("boom", 0.1f);

            }

        }
       
    }
    void boom()
    {
        Destroy(gameObject);
    }
}
