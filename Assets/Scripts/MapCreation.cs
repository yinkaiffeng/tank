using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class MapCreation : MonoBehaviour
{
    //用来装饰地图初始化所需物体的数组；
    //0.老家，1墙，2障碍，3出生效果，4河流，5草，空气墙
    public GameObject[] item;
    //已经有东西的位置列表
    private List<Vector3> itempositionList = new List<Vector3>();

    private void Awake()
    {
        InitMap();
    }

    private void InitMap()
    {
        //实例化老家
        CreatItem(item[0], new Vector3(0,-8,0), quaternion.identity);
        //实例化墙把老家围起来
        CreatItem(item[1], new Vector3(-1, -8, 0), quaternion.identity);
        CreatItem(item[1], new Vector3(1, -8, 0),quaternion.identity);
        for (int i = -1; i < 2; i++)
        {
            CreatItem(item[1],new Vector3(i,-7,0),quaternion.identity);
        }
        //实例化外围墙
        for (int i = -11; i < 12; i++)
        {
            CreatItem(item[6],new Vector3(i,9,0),quaternion.identity);
        }
        for (int i = -11; i < 12; i++)
        {
            CreatItem(item[6],new Vector3(i,-9,0),quaternion.identity);
        }
       
        for (int i = -8; i < 9; i++)
        {
            CreatItem(item[6],new Vector3(-11,i,0),quaternion.identity);
        }
        for (int i = -8; i < 9; i++)
        {
            CreatItem(item[6],new Vector3(11,i,0),quaternion.identity);
        }
        //实例化地图
        for (int i = 0; i < 20; i++)
        {
            CreatItem(item[1], CreateRandomPosition(),quaternion.identity);
        }
        for (int i = 0; i < 20; i++)
        {
            CreatItem(item[2], CreateRandomPosition(),quaternion.identity);
        }
        for (int i = 0; i < 20; i++)
        {
            CreatItem(item[4], CreateRandomPosition(),quaternion.identity);
        }
        for (int i = 0; i < 20; i++)
        {
            CreatItem(item[5], CreateRandomPosition(),quaternion.identity);
        }
        //初始化玩家
        GameObject go = Instantiate(item[3], new Vector3(-2, -8, 0), Quaternion.identity);
        go.GetComponent<Born>().creatPlayer = true;
        
        //产生敌人
        Instantiate(item[3], new Vector3(-10, 8, 0), Quaternion.identity);
        Instantiate(item[3], new Vector3(0, 8, 0), Quaternion.identity);
        Instantiate(item[3], new Vector3(10, 8, 0), Quaternion.identity);
        InvokeRepeating("CreateEnemy",4,5);
    }

    private void CreatItem(GameObject creatGameObject,Vector3 creatPosition,quaternion creatRotation)
    {
        GameObject itemGo = Instantiate(creatGameObject, creatPosition, creatRotation);
        itemGo.transform.SetParent(gameObject.transform);
        itempositionList.Add(creatPosition);
    }
    //产生随机位置的方法
    private Vector3 CreateRandomPosition()
    {
        //不生成 X= -10 ，10 的两列 ，y = -8,8 整的两行的位置；
         while (true)
         {
             Vector3 createPosition = new Vector3(Random.Range(-9, 10), Random.Range(-7, 8), 0);
             if (!HasThePosition(createPosition))
             {
                 return createPosition;
             }

         }
    }
    
   private bool HasThePosition(Vector3 createpos)
    {
        for (int i = 0; i < itempositionList.Count; i++)
        {
            if (createpos==itempositionList[i])
            {
                return true;
            }
        }

        return false;
    }
   
   //产生敌人的方法
   private void CreateEnemy()
   {
       int num = Random.Range(0, 3);
       Vector3 EnemyPos = new Vector3();
       if (num==0)
       {
           EnemyPos = new Vector3(-10, 8, 0);
       }
       if (num==1)
       {
           EnemyPos = new Vector3(0, 8, 0);
       }
       if (num==2)
       {
           EnemyPos = new Vector3(10, 8, 0);
       }
       CreatItem(item[3],EnemyPos,quaternion.identity);
   }
}
