using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
 //属性
     public float moveSpeed = 3;
     private Vector3 bullectEulerAngles;
     private float v = -1;
     private float h;
    
 

     //引用
     private SpriteRenderer sr;
     public GameObject bullectPrefab;
     public Sprite[] tankSprite;
   
     public GameObject explosionPrefab;
     
     //计时器
     private float timeVal = 3;
     private float timeyalChangeDirection = 0;
     
     
     private void Awake()
     {
         sr = GetComponent<SpriteRenderer>();
     }

     // Update is called once per frame
     void Update()
     {   
         
         //攻击的时间间隔
         if (timeVal>3)
         {
             Attack();
         }
         else
         {
             timeVal += Time.deltaTime;
         }

     }

     private void FixedUpdate()
     {
         Move();
     }

     //坦克的攻击方法
         void Attack()
         {
          
                 //子弹产生的角度：当前坦克的角度+子弹应该旋转的角度
                 Instantiate(bullectPrefab, transform.position, Quaternion.Euler(transform.eulerAngles+bullectEulerAngles));
                 timeVal = 0;
             
         }

         //坦克移动方法
         void Move()
         {
             if (timeyalChangeDirection >= 4)
             {
                 int num = Random.Range(0, 8);
                 if (num >= 5)
                 {
                     this.v = -1;
                     this.h = 0;
                 }
                 else if (num == 0)
                 {
                     v = 1;
                     h = 0;
                 }
                 else if (num > 0 && num <= 2)
                 {
                     h = -1;
                     v = 0;
                 }
                 else if (num > 2 && num <= 4)
                 {
                     h = 1;
                     v = 0;
                 }

                 timeyalChangeDirection = 0;
             }
             else
                 {
                     timeyalChangeDirection += Time.fixedDeltaTime;
                 
             }
           //  h = Input.GetAxisRaw("Horizontal");
             transform.Translate(Vector3.right * moveSpeed * Time.fixedDeltaTime * h, Space.World);
             if (h < 0)
             {
                 sr.sprite = tankSprite[3];
                 bullectEulerAngles = new Vector3(0, 0, 90);
             }

             if (h > 0)
             {
                 sr.sprite = tankSprite[1];
                 bullectEulerAngles = new Vector3(0, 0, -90);
             }

             if (h != 0)
             {
                 return;
             }

           // v = Input.GetAxisRaw("Vertical");
             transform.Translate(Vector3.up * moveSpeed * Time.fixedDeltaTime * v, Space.World);

             if (v < 0)
             {
                 sr.sprite = tankSprite[2];
                 bullectEulerAngles = new Vector3(0, 0, 180);
             }

             if (v > 0)
             {
                 sr.sprite = tankSprite[0];
                 bullectEulerAngles = new Vector3(0, 0, 0);
             }

         } 
         
         //坦克的死亡方法

         private void Die()
         {
             PlayerMananger.Instance.playerScore++;
             //产生爆炸特效
             Instantiate(explosionPrefab,transform.position,transform.rotation);
             //死亡
             Destroy(gameObject);
         }
         //两个敌人碰撞瞬间转向
         private void OnCollisionEnter2D(Collision2D col)
         {
             if (col.gameObject.tag == "Enemy")

             {
                 timeyalChangeDirection = 4;
             }
         }
}
