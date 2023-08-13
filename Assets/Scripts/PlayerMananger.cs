using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMananger : MonoBehaviour
{
   //属性值
   public int LifeValue = 3;
   public int playerScore = 0;
   //引用
   public GameObject born;
   public Text playerScoreText;
   public Text PlayerLifeValueText;
   public GameObject isDefeatUI;
   

   public bool isDead;

   public bool isDefeat;
   //单例
   private static PlayerMananger instance;

   public static PlayerMananger Instance
   {
      get { return instance;}
      set { instance = value; }
   }

   private void Update()
   {
      if (isDefeat == true)
      {
         isDefeatUI.SetActive(true);
         Invoke("ReturnToTheMainMenu",3);
      }
      if (isDead==true)
      {
         Recover();
      }

      playerScoreText.text = playerScore.ToString();
      PlayerLifeValueText.text = LifeValue.ToString();

   }

   private void Awake()
   {
      instance = this;
   }

   private void Recover()
   {
      if (LifeValue <= 0)
      {
         //游戏失败，返回主界面
         isDefeat = true;
         Invoke("ReturnToTheMainMenu",3);
      }
      else
      {
         LifeValue--;
         GameObject go = Instantiate(born, new Vector3(-2, -8, 0), Quaternion.identity);
         go.GetComponent<Born>().creatPlayer = true;
         isDead = false;

      }
   }

   private void ReturnToTheMainMenu()
   {
      SceneManager.LoadScene(0);
   }

}
