using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissDetecter : MonoBehaviour
{
    public GameObject missObj;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Instantiate(missObj, new Vector2(collision.transform.position.x, -4.5f), collision.transform.rotation);
        Destroy(collision.gameObject);
        GameController.instance.combo = 0;
    }

}
