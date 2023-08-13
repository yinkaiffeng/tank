using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullect : MonoBehaviour
{
    public float moveSpeed = 0;

    public bool isPlayerBullect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.up*moveSpeed*Time.deltaTime,Space.World);
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        switch (col.tag)
        {
            case "Tank":
                if (!isPlayerBullect)
                {
                    col.SendMessage("Die");
                    Destroy(gameObject);
                }
              
                
                break;
            case "Heart":
                col.SendMessage("Die");
                Destroy(gameObject);
                break;
            case "Enemy":
                if (isPlayerBullect)
                {
                    col.SendMessage("Die");
                    Destroy(gameObject);
                }
                break;
            case "Wall":
                Destroy(col.gameObject);
                Destroy(gameObject);
                break;
            case "Barriar":
                if (isPlayerBullect)
                {
                    col.SendMessage("PlayAudio");
                }
                
                Destroy(gameObject);
                break;
           default:
                break;
        }
    }
}
