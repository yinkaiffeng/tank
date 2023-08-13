 using System;
 using System.Collections;
using System.Collections.Generic;
 using System.Security;
 using Unity.Mathematics;
 using Unity.VisualScripting.Dependencies.NCalc;
 using UnityEngine;
 using UnityEngine.Timeline;

 public class Player : MonoBehaviour
 {
     //属性
     public float moveSpeed = 3;
     private Vector3 bullectEulerAngles;
     private float timeVal = 3;
     private bool isDefend = true;

     //引用
     private SpriteRenderer sr;
     public GameObject bullectPrefab;
     public Sprite[] tankSprite;
     private float defendTimeVal = 3;
     public GameObject explosionPrefab;
     public GameObject defendEffectPrefab;
     public AudioSource moveAudio;
     public AudioClip[] TankAudio;

     // Start is called before the first frame update
     void Start()
     {

     }

     private void Awake()
     {
         sr = GetComponent<SpriteRenderer>();
     }

     // Update is called once per frame
     void Update()
     {   //保护是否处于无敌状态
         if (isDefend)
         {
             defendEffectPrefab.SetActive(true);
             defendTimeVal -= Time.deltaTime;
             if (defendTimeVal<=0)
             {
                
                 isDefend = false;
                 defendEffectPrefab.SetActive(false);
             }
         }
         
       

     }

     private void FixedUpdate()
     {
         if (PlayerMananger.Instance.isDefeat)
         {
             return;
         }
         Move();
         //攻击的CD
         if (timeVal>0.4f)
         {
             Attack();
         }
         else
         {
             timeVal += Time.deltaTime;
         }
        
     }

     //坦克的攻击方法
         void Attack()
         {
             if (Input.GetKeyDown(KeyCode.Space))
             {
                 //子弹产生的角度：当前坦克的角度+子弹应该旋转的角度
                 Instantiate(bullectPrefab, transform.position, Quaternion.Euler(transform.eulerAngles+bullectEulerAngles));
                 timeVal = 0;
             }
         }

         //坦克移动方法
         void Move()
         {
             float h = Input.GetAxisRaw("Horizontal");
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
             if (Mathf.Abs(h)>0.05f)
             {
                 moveAudio.clip = TankAudio[1];
               
                 if (!moveAudio.isPlaying)
                 {
                     moveAudio.Play();
                 }
             }

             float v = Input.GetAxisRaw("Vertical");
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

             if (Mathf.Abs(v)>0.05f)
             {
                 moveAudio.clip = TankAudio[1];
               
                 if (!moveAudio.isPlaying)
                 {
                     moveAudio.Play();
                 } 
             }
             
             else
             {
                 moveAudio.clip = TankAudio[0];
               
                 if (!moveAudio.isPlaying)
                 {
                     moveAudio.Play();
                 }
             }
            

         } 
         
         //坦克的死亡方法

         private void Die()
         {
           
             if (isDefend)
             {
                 return;
             }
             //玩家生命值减一调用
             PlayerMananger.Instance.isDead = true;
             //产生爆炸特效
             Instantiate(explosionPrefab,transform.position,transform.rotation);
             //死亡
             Destroy(gameObject);
         }
 }
