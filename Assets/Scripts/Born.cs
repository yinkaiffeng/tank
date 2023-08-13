using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Born : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject[] enemyPrefablist;
    public bool creatPlayer;
    void Start()
    {
        Invoke("BornTank",1f);  //延时0.8 秒调用
        Destroy(gameObject,1f);
    }
    

    void BornTank()
    {
        if (creatPlayer)
        {
            Instantiate(playerPrefab, transform.position, quaternion.identity);
        }
        else
        {
            int num = Random.Range(0, 2);
            Instantiate(enemyPrefablist[num], transform.position, quaternion.identity);
            
        }
    }

}
