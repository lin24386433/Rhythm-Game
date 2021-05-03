using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongNote : MonoBehaviour
{
    public GameObject startNote;
    public GameObject middleNote;
    public GameObject endNote;

    public bool OnFalling = true;
    public bool OnHolding = false;

    public float noteLength = 3f;

    //public float speed = 3f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        middleNote.transform.localScale = new Vector3(middleNote.transform.localScale.x, noteLength, middleNote.transform.localScale.z);

        middleNote.transform.localPosition = startNote.transform.localPosition + new Vector3(0, noteLength / 2, 0);
        endNote.transform.localPosition = startNote.transform.localPosition + new Vector3(0, noteLength, 0);


        if (OnFalling)
        {
            OnFallingFunc();
        }
        else if (OnHolding && noteLength >= -1f)
        {
            OnHoldingFunc();
        }

        if(noteLength <= -1f)
        {
            Destroy(this.gameObject);
        }    
        
    }

    void OnFallingFunc()
    {
        transform.Translate(0, -(GameController.instance.beatPerSec) * Time.deltaTime, 0);
        //transform.Translate(0, -(speed) * Time.deltaTime, 0);
    }

    void OnHoldingFunc()
    {
        noteLength -= GameController.instance.beatPerSec * Time.deltaTime;
        //noteLength -= speed * Time.deltaTime;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        StartCoroutine(waittime(0f));

    }

    IEnumerator waittime(float mytime)
    {
        yield return new WaitForSeconds(mytime); // ����x��
        GameController.instance.combo = 0;
        GameController.instance.timingTxt.text = "Miss";
        GameController.instance.timingTxt.color = Color.white;
        Destroy(this.gameObject);
    }
}
