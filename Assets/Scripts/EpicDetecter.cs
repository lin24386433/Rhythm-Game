using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EpicDetecter : MonoBehaviour
{
    [SerializeField]
    public AudioSource musicSource;

    private void Start()
    {
        musicSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        musicSource.Play();

    }
}
