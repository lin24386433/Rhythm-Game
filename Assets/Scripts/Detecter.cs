using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Detecter : MonoBehaviour
{
    AudioSource musicSource;

    public SpriteRenderer sprites;

    

    public bool canDestroy = false;

    public KeyCode keyCodes;

    Color whiteAlphaFull;
    Color whiteAlphaHalf;

    // Start is called before the first frame update
    void Start()
    {
        musicSource = GetComponent<AudioSource>();

        sprites = GetComponent<SpriteRenderer>();

        whiteAlphaFull = Color.white;
        whiteAlphaFull.a = 0.7f;

        whiteAlphaHalf = Color.white;
        whiteAlphaHalf.a = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        DetecterHandler();
    }

    void DetecterHandler()
    {
        if (Input.GetKeyDown(keyCodes))
        {
            sprites.color = whiteAlphaFull;
            canDestroy = true;
        }
        if (Input.GetKeyUp(keyCodes))
        {
            sprites.color = whiteAlphaHalf;
            canDestroy = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        
        if (canDestroy)
        {
            float timing = collision.transform.position.y - this.transform.position.y;
            if (timing < 0) timing *= -1;

            if(timing <= 0.1f)
            {
                GameController.instance.timingTxt.text = "Perfect";
                GameController.instance.timingTxt.color = Color.yellow;
                GameController.instance.gameScore += 500;
            }
            else if(timing <= 0.3f)
            {
                GameController.instance.timingTxt.text = "Good";
                GameController.instance.timingTxt.color = Color.green;
                GameController.instance.gameScore += 300;
            }
            else if(timing <= 0.5f)
            {
                GameController.instance.timingTxt.text = "Bad";
                GameController.instance.timingTxt.color = Color.red;
                GameController.instance.gameScore += 100;
            }

            GameController.instance.combo++;
            musicSource.Play();
            Destroy(collision.gameObject);
        }
        
        /*
        musicSource.Play();
        Destroy(collision.gameObject);
        */
    }

}
