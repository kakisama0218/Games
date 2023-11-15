using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePopup : MonoBehaviour
{
    private TextMeshPro TextMesh;
    private Color TextColor;
    private float DisappearTimer;
    // Start is called before the first frame update
    void Awake()
    {
        TextMesh = transform.GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveSpeed = 2f;
        transform.position += new Vector3(0, moveSpeed) * Time.deltaTime;
        DisappearTimer -= Time.deltaTime;
        if(DisappearTimer<0)
        {
            float disappearSpeed = 10f;
            TextColor.a -= disappearSpeed * Time.deltaTime;
            TextMesh.color = TextColor;
            if(TextColor.a<0)
            {
                Destroy(gameObject);
            }
        }
    }

    public void SetUp(int damageAmount)
    {
        TextMesh.SetText(damageAmount.ToString());
        transform.position += new Vector3(0, 1);
        TextColor = TextMesh.color;
        DisappearTimer = 0.5f;
    }
}
