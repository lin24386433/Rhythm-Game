using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Detecter : MonoBehaviour
{
    AudioSource musicSource;

    public GameObject[] effects;

    public GameObject lightEffect;
    public GameObject lightBG;

    public bool canDestroy = false;

    public KeyCode keyCodes;

    public float perfectTiming = 0.5f;
    public float goodTiming = 0.7f;
    public float badTiming = 0.9f;


    void Start()
    {
        musicSource = GetComponent<AudioSource>();

        lightEffect.SetActive(false);
        lightBG.SetActive(false);
    }

    void Update()
    {
        DetecterHandler();
    }

    void DetecterHandler()
    {
        if (Input.GetKeyDown(keyCodes))
        {
            lightEffect.SetActive(true);
            lightBG.SetActive(true);

            canDestroy = true;
        }
        if (Input.GetKeyUp(keyCodes))
        {
            lightEffect.SetActive(false);
            lightBG.SetActive(false);

            canDestroy = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // canDestroy is true
        if (canDestroy)
        {
            float timing = collision.transform.position.y - this.transform.position.y;
            if (timing < 0) timing *= -1;

            if (collision.tag == "Note")
            {
                if (timing <= perfectTiming)
                {
                    GameController.instance.gameScore += 500;
                    GameData.perfectCount++;
                    GameController.instance.combo++;
                    Instantiate(effects[0], this.transform.position, this.transform.rotation);
                }
                else if (timing <= goodTiming)
                {
                    GameController.instance.gameScore += 300;
                    GameData.goodCount++;
                    GameController.instance.combo++;
                    Instantiate(effects[1], this.transform.position, this.transform.rotation);
                }
                else if (timing <= badTiming)
                {
                    GameController.instance.gameScore += 100;
                    GameData.badCount++;
                    GameController.instance.combo = 0;
                    Instantiate(effects[2], this.transform.position, this.transform.rotation);
                }

                
                musicSource.Play();
                Destroy(collision.gameObject);
            }
            else if(collision.tag == "LongNoteStart")
            {
                collision.gameObject.transform.parent.gameObject.GetComponent<LongNote>().OnFalling = false;
                collision.gameObject.transform.parent.gameObject.GetComponent<LongNote>().OnHolding = true;

                // to keep long note's length when timeing isn't accurate at all
                if(collision.transform.position.y - this.transform.position.y >= 0)
                    collision.gameObject.transform.parent.gameObject.GetComponent<LongNote>().noteLength += timing;
                else
                    collision.gameObject.transform.parent.gameObject.GetComponent<LongNote>().noteLength -= timing;

                collision.gameObject.transform.parent.gameObject.transform.position = this.transform.position;

                if (timing <= perfectTiming)
                {
                    GameController.instance.gameScore += 500;
                    GameData.perfectCount++;
                    GameController.instance.combo++;
                    Instantiate(effects[0], this.transform.position, this.transform.rotation);
                    
                }
                else if (timing <= goodTiming)
                {
                    GameController.instance.gameScore += 300;
                    GameData.goodCount++;
                    GameController.instance.combo++;
                    Instantiate(effects[1], this.transform.position, this.transform.rotation);
                }
                else if (timing <= badTiming)
                {
                    GameController.instance.gameScore += 100;
                    GameData.badCount++;
                    GameController.instance.combo = 0;
                    Instantiate(effects[2], this.transform.position, this.transform.rotation);
                }

                
                musicSource.Play();
                collision.tag = "LongNoteStarted";

            }
            else if (collision.tag == "LongNoteMiddle")
            {
                Instantiate(effects[4], this.transform.position, this.transform.rotation);
                //GameController.instance.gameScore += 17;
            }
        }
        // canDestroy is false
        else            
        {
            if (collision.tag == "LongNoteStarted" && collision.gameObject.transform.parent.gameObject.GetComponent<LongNote>().OnHolding)
            {
                Destroy(collision.gameObject.transform.parent.gameObject);

                if(collision.gameObject.transform.parent.gameObject.GetComponent<LongNote>().noteLength >= badTiming)
                {
                    GameController.instance.combo = 0;
                    GameData.missCount++;
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
                    GameData.perfectCount++;
                    GameController.instance.combo++;
                    Instantiate(effects[0], this.transform.position, this.transform.rotation);
                }
                else if (timing <= goodTiming)
                {
                    GameController.instance.gameScore += 300;
                    GameData.goodCount++;
                    GameController.instance.combo++;
                    Instantiate(effects[1], this.transform.position, this.transform.rotation);
                }
                else if (timing <= badTiming)
                {
                    GameController.instance.gameScore += 100;
                    GameData.badCount++;
                    GameController.instance.combo = 0;
                    Instantiate(effects[2], this.transform.position, this.transform.rotation);
                }

                
                musicSource.Play();
            }
        }
        
    }

}
