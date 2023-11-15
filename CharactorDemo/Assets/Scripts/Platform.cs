using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
  public float moveSpeed;
  private float waitTime;
  public float totalTime;
    public GameObject Player;
  public Transform[] movePos;
  private Transform playerTransform;
  //i是1则右，是0则变成左
  private int i;
  void Start()
  {

    //为了使项目结构看着舒服些，开始将移动平台的左右两个判断点放置于移动平台的子物体中，
    //开局自动解除父子关系，否则两侧的判断点会跟着移动平台一起移动
    for (int j = 0; j < transform.childCount+1; j++)
    {
      //Debug.Log(j);
      //开局直接断绝所有父子关系
      movePos[j].gameObject.transform.parent = playerTransform;
      
    }

        playerTransform = Player.transform.parent;
        //playerTransform = GameObject.FindGameObjectWithTag("Player").transform.parent;
    i = 1;
    waitTime = totalTime;
  }

  void Update()
  {
    transform.position = Vector2.MoveTowards(transform.position, movePos[i].position, moveSpeed * Time.deltaTime);


    //如果两点的距离小于等于0.1
    if (Vector2.Distance(transform.position, movePos[i].position) <= 0.1f)
    {
      //且等待时间小于0
      if (waitTime < 0)
      {
        if (i == 1)
        {
          i = 0;
        }
        else
        {
          i = 1;
        }
        waitTime = totalTime;
      }
      else
      {
        waitTime -= Time.deltaTime;
      }
    }
  }


  //当角色和移动平台碰撞时，将角色设置为移动平台的子角色，从而形成角色同平台一起移动
  private void OnTriggerEnter2D(Collision2D other)
  {
        //other.gameObject.transform.parent = playerTransform;
        if (other.gameObject.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.Collision2D")
        {
            //将movingPlateform作为player的父对象
            other.gameObject.transform.parent = gameObject.transform;
        }
  }
  //角色和移动平台取消碰撞时断绝父子关系
  private void OnTriggerExit2D(Collision2D other)
  {
        if (other.gameObject.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.Collision2D")
        {
            //将movingPlateform作为player的父对象
            other.gameObject.transform.parent = playerTransform;
        }

  }
}