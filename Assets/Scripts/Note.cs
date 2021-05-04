using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    void Update()
    {
        transform.Translate(0, -(GameController.instance.beatPerSec) * Time.deltaTime, 0);
    }

}
