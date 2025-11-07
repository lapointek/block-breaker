using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{

    //Config Parameters
    [SerializeField] AudioClip breakSound;
    [SerializeField] GameObject blockVFX;
    [SerializeField] Sprite[] hitSprites;

    //cached reference
    Level level;

    //State Variables
    [SerializeField] int timesHit; // only serialized for debug purposes.

    private void Start()
    {
        CountBreakableBlocks();
    }

    private void CountBreakableBlocks()
    {
        level = FindObjectOfType<Level>();
        if(tag == "Breakable")
        {
            level.CountBlocks();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if(tag == "Breakable")
        {
            HandleHit();
        }
    }

    private void HandleHit()
    {
        timesHit++;
        int maxHits = hitSprites.Length + 1;
        if(timesHit >= maxHits)
        {
            DestroyBlock();
        }

        else
        {
            ShowNextHitSprite();
        }
    }

    private void ShowNextHitSprite()
    {
        int spriteIndex = timesHit - 1;
        if(hitSprites[spriteIndex] != null) //not null or is Valid
        {
            GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
        }

        else
        {
            Debug.LogError("Block Sprite is missing from array" + gameObject.name);
        }
    }

    private void DestroyBlock()
    {
        PlayBlockDestroySFX();
        Destroy(gameObject);
        TriggerParticlesVFX();
        level.BlockDestroyed();
    }

    private void PlayBlockDestroySFX()
    {
        FindObjectOfType<GameSession>().AddToScore();
        AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position);
    }

    private void TriggerParticlesVFX()
    {
        GameObject particles = Instantiate(blockVFX, transform.position, transform.rotation);
        Destroy(particles, 1f);
    }
}
