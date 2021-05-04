﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Detecter : MonoBehaviour
{
    AudioSource musicSource;

    public SpriteRenderer sprites;

    public GameObject[] effects;

    public GameObject light;
    public GameObject lightBG;

    public bool canDestroy = false;

    public KeyCode keyCodes;

    public float perfectTiming = 0.5f;
    public float goodTiming = 0.7f;
    public float badTiming = 0.9f;

    Color whiteAlphaFull;
    Color whiteAlphaHalf;

    void Start()
    {
        musicSource = GetComponent<AudioSource>();

        light.SetActive(false);
        lightBG.SetActive(false);

        /*
        sprites = GetComponent<SpriteRenderer>();

        whiteAlphaFull = Color.white;
        whiteAlphaFull.a = 0.7f;

        whiteAlphaHalf = Color.white;
        whiteAlphaHalf.a = 1f;
        */
    }

    void Update()
    {
        DetecterHandler();
    }

    void DetecterHandler()
    {
        if (Input.GetKeyDown(keyCodes))
        {
            light.SetActive(true);
            lightBG.SetActive(true);
            //sprites.color = whiteAlphaFull;
            canDestroy = true;
        }
        if (Input.GetKeyUp(keyCodes))
        {
            light.SetActive(false);
            lightBG.SetActive(false);
            //sprites.color = whiteAlphaHalf;
            canDestroy = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        
        if (canDestroy)
        {
            float timing = collision.transform.position.y - this.transform.position.y;
            if (timing < 0) timing *= -1;

            if (collision.tag == "Note")
            {
                if (timing <= perfectTiming)
                {
                    GameController.instance.gameScore += 500;
                    Instantiate(effects[0], this.transform.position, this.transform.rotation);
                }
                else if (timing <= goodTiming)
                {
                    GameController.instance.gameScore += 300;
                    Instantiate(effects[1], this.transform.position, this.transform.rotation);
                }
                else if (timing <= badTiming)
                {
                    GameController.instance.gameScore += 100;
                    Instantiate(effects[2], this.transform.position, this.transform.rotation);
                }

                GameController.instance.combo++;
                musicSource.Play();
                Destroy(collision.gameObject);
            }
            else if(collision.tag == "LongNoteStart")
            {
                collision.gameObject.transform.parent.gameObject.GetComponent<LongNote>().OnFalling = false;
                collision.gameObject.transform.parent.gameObject.GetComponent<LongNote>().OnHolding = true;

                if(collision.transform.position.y - this.transform.position.y >= 0)
                    collision.gameObject.transform.parent.gameObject.GetComponent<LongNote>().noteLength += timing;
                else
                    collision.gameObject.transform.parent.gameObject.GetComponent<LongNote>().noteLength -= timing;

                collision.gameObject.transform.parent.gameObject.transform.position = this.transform.position;

                if (timing <= perfectTiming)
                {
                    GameController.instance.gameScore += 500;
                    Instantiate(effects[0], this.transform.position, this.transform.rotation);
                    
                }
                else if (timing <= goodTiming)
                {
                    GameController.instance.gameScore += 300;
                    Instantiate(effects[1], this.transform.position, this.transform.rotation);
                }
                else if (timing <= badTiming)
                {
                    GameController.instance.gameScore += 100;
                    Instantiate(effects[2], this.transform.position, this.transform.rotation);
                }

                GameController.instance.combo++;
                musicSource.Play();
                collision.tag = "LongNoteStarted";

            }
            else if (collision.tag == "LongNoteMiddle")
            {
                Instantiate(effects[4], this.transform.position, this.transform.rotation);
                //GameController.instance.gameScore += 17;
            }
        }
        else
        {
            if (collision.tag == "LongNoteStarted" && collision.gameObject.transform.parent.gameObject.GetComponent<LongNote>().OnHolding)
            {
                Destroy(collision.gameObject.transform.parent.gameObject);

                if(collision.gameObject.transform.parent.gameObject.GetComponent<LongNote>().noteLength >= 0.5f)
                {
                    GameController.instance.combo = 0;
                    Instantiate(effects[3], new Vector2(this.transform.position.x, -4.5f), this.transform.rotation);
                }
                
            }
            if (collision.tag == "LongNoteEnd")
            {
                float timing = collision.transform.position.y - this.transform.position.y;
                if (timing < 0) timing *= -1;

                if (timing <= perfectTiming)
                {
                    GameController.instance.gameScore += 500;
                    Instantiate(effects[0], this.transform.position, this.transform.rotation);
                }
                else if (timing <= goodTiming)
                {
                    GameController.instance.gameScore += 300;
                    Instantiate(effects[1], this.transform.position, this.transform.rotation);
                }
                else if (timing <= badTiming)
                {
                    GameController.instance.gameScore += 100;
                    Instantiate(effects[2], this.transform.position, this.transform.rotation);
                }

                GameController.instance.combo++;
                musicSource.Play();
            }
        }
        
    }

}
