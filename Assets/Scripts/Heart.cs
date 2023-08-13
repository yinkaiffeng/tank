using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    private SpriteRenderer sr;
    public Sprite BrokenSprite;
    public GameObject explosionPrefab;
    public AudioClip dieAudio;
    
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Die()
    {
        sr.sprite = BrokenSprite;
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        PlayerMananger.Instance.isDefeat = true;
        AudioSource.PlayClipAtPoint(dieAudio,transform.position);


    }
}
