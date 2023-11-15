using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : MonoBehaviour
{
    public float interval;
    public GameObject bulletPerfab;
    //public GameObject shellPrefab;

    public Transform startPos;
    public Transform endPos;

    //private Vector2 mousePos;
    private Vector2 direction;

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shoot()
    {
        direction = (new Vector2(endPos.transform.position.x, endPos.transform.position.y) - new Vector2(startPos.transform.position.x, startPos.transform.position.y)).normalized;
        Fire();
    }
    void Fire()
    {
        GameObject bullet = Instantiate(bulletPerfab, startPos.position, Quaternion.identity);
        bullet.GetComponent<FireBall>().SetSpeed(direction);
    }
}
