using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parallax : MonoBehaviour
{
    public Transform Cam;
    public float moveRate;
    private float startPointX, startPointY;
    public bool lockY;
    // Start is called before the first frame update
    void Start()
    {
        startPointX = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (lockY)
        {
            transform.position = new Vector2(startPointX + Cam.position.x * moveRate, transform.position.y);
        }
        else
        {
            transform.position = new Vector2(startPointX + Cam.position.x * moveRate, startPointY + Cam.position.y * moveRate);
        }
    }
}
