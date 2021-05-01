using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongNote : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, -(GameController.instance.beatPerSec) * Time.deltaTime, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        StartCoroutine(waittime(0.5f));

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
