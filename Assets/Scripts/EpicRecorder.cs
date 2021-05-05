using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EpicRecorder : MonoBehaviour
{
    // Song beats per minute
    // This is determined by the song you're trying to sync up to
    public float songBpm;

    // The number of seconds for each song beat
    public float secPerBeat;

    // The number of beats for each second
    public float beatPerSec;

    // Current song position, in seconds
    public float songPosition;

    // Current song position, in beats
    public float songPositionInBeats = 0;

    public int beatNow = 0;

    // How many seconds have passed since the song started
    public float dspSongTime;

    // an AudioSource attached to this GameObject that will play the music.
    public AudioSource musicSource;

    public int totalBeats;

    public float beatInScrollBar;

    public float musicLength;

    public float time;

    public int[,] notes = new int[804, 4]
    {
        {0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,2,0,1},
{0,0,0,0},
{0,0,0,0},
{0,0,1,0},
{0,0,0,0},
{0,0,0,0},
{0,2,0,1},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,2,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,1},
{0,0,0,0},
{0,0,0,0},
{1,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,2,0},
{0,0,0,0},
{0,1,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,1},
{0,0,0,0},
{0,0,0,0},
{1,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,1,0},
{0,0,0,1},
{0,1,0,0},
{1,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,1},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{1,0,0,0},
{0,0,0,0},
{0,0,1,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,1,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,1},
{0,0,0,0},
{0,0,0,0},
{1,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,2,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,2,0},
{0,0,0,0},
{0,0,0,0},
{2,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,2},
{0,0,0,0},
{0,0,0,0},
{0,0,1,0},
{0,0,0,0},
{0,0,0,0},
{2,0,0,0},
{0,0,0,0},
{0,0,0,2},
{0,0,0,0},
{0,1,0,0},
{0,0,0,0},
{0,0,1,0},
{0,0,0,0},
{0,0,0,0},
{0,0,1,0},
{0,0,0,0},
{0,0,0,0},
{1,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,2},
{0,0,0,0},
{0,1,0,0},
{0,0,1,0},
{0,0,0,0},
{0,0,0,0},
{1,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,2},
{0,0,0,0},
{0,0,0,0},
{0,0,0,1},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{1,0,0,0},
{0,0,0,0},
{0,0,1,0},
{0,0,0,0},
{0,1,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,1},
{0,0,0,0},
{0,0,0,0},
{0,0,0,1},
{0,0,0,0},
{0,0,0,0},
{1,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,1,0},
{0,0,0,0},
{0,2,0,0},
{0,0,0,0},
{0,0,0,1},
{0,0,0,0},
{0,2,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,1},
{0,0,0,0},
{0,0,0,0},
{1,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,1,0},
{0,1,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,2},
{0,0,0,0},
{1,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{1,0,0,0},
{0,0,0,0},
{0,0,1,0},
{0,0,0,0},
{0,1,0,0},
{0,0,0,0},
{1,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,2},
{0,0,0,0},
{0,0,0,0},
{1,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,1,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,1},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,2,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{1,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,2,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,1,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{1,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,1},
{0,0,0,0},
{0,0,0,0},
{0,1,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,1,0},
{0,0,0,0},
{0,0,0,0},
{1,0,0,0},
{0,0,0,0},
{0,0,0,1},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,1,0,0},
{0,0,0,0},
{0,0,1,0},
{0,0,0,0},
{0,0,0,1},
{0,0,0,0},
{0,0,0,1},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{1,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,1,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,1,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,2,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,2,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,1},
{0,0,0,0},
{1,0,0,0},
{0,0,0,0},
{0,0,1,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,1,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,2},
{0,0,0,0},
{0,0,0,0},
{1,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,1,0},
{0,0,0,0},
{0,1,0,0},
{0,0,0,0},
{0,0,0,2},
{0,0,0,0},
{1,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{1,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{1,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{1,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,1},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,1,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,1,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,1},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{1,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,1,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,1,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,1},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,1,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,1,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{1,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,1},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,1,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,1,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{2,0,0,0},
{0,0,0,0},
{2,0,0,0},
{0,0,0,0},
{0,1,0,0},
{0,0,0,0},
{0,0,0,1},
{0,0,0,0},
{0,0,0,0},
{0,0,1,0},
{0,0,0,0},
{0,0,0,0},
{1,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,1,0,0},
{0,0,0,0},
{0,0,1,0},
{0,0,0,0},
{0,0,0,0},
{1,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,1},
{0,0,0,0},
{0,1,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,1,0},
{0,0,0,0},
{0,0,0,0},
{1,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,1},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{1,0,0,1},
{0,0,0,0},
{0,0,0,0},
{0,1,1,0},
{0,0,0,0},
{0,0,0,0},
{1,0,0,1},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,1,0},
{1,1,0,1},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{1,0,0,0},
{0,0,0,0},
{0,1,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,1},
{0,0,1,0},
{1,1,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,1},
{0,0,0,0},
{0,0,0,0},
{0,0,1,0},
{0,0,0,0},
{0,1,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,1,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,1,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,1,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,1,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,1,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,1,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,1,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,1},
{0,0,0,0},
{0,1,0,0},
{0,0,0,0},
{0,0,0,1},
{0,0,0,0},
{0,1,0,0},
{0,0,0,0},
{0,0,0,1},
{0,0,0,0},
{0,1,0,0},
{0,0,0,0},
{0,0,0,1},
{0,0,0,0},
{0,1,0,0},
{0,0,0,0},
{0,0,1,0},
{0,0,0,0},
{0,0,0,0},
{1,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,1,0,0},
{0,0,0,0},
{0,0,0,1},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,1,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,1,0},
{0,0,0,0},
{1,0,0,0},
{0,0,0,0},
{0,0,0,1},
{0,0,0,0},
{0,1,0,0},
{0,0,0,0},
{0,0,1,0},
{0,0,0,0},
{1,0,1,0},
{0,0,0,0},
{1,0,1,0},
{0,0,0,0},
{0,0,0,1},
{0,0,0,0},
{0,0,0,0},
{0,1,0,0},
{0,0,1,0},
{0,0,0,0},
{0,0,0,0},
{1,0,0,0},
{0,0,0,1},
{0,0,0,0},
{0,1,0,0},
{0,0,0,0},
{0,0,0,1},
{0,0,1,0},
{0,0,1,1},
{0,0,0,0},
{0,0,0,0},
{0,0,1,0},
{0,0,0,0},
{1,0,0,0},
{0,0,0,0},
{0,0,0,1},
{0,1,0,0},
{0,0,0,0},
{0,0,1,0},
{0,0,0,0},
{0,0,0,0},
{1,0,0,0},
{0,0,0,0},
{0,0,0,2},
{0,0,0,0},
{0,1,0,0},
{0,0,0,0},
{0,0,1,0},
{1,0,0,0},
{0,0,0,2},
{0,0,0,0},
{0,0,0,0},
{0,1,1,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{1,0,0,1},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{2,1,1,2},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{2,0,0,2},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,1,0,0},
{0,0,0,0},
{0,0,1,0},
{0,0,0,0},
{0,0,0,0},
{1,0,0,0},
{0,0,0,0},
{0,0,0,1},
{0,1,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,1,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{1,0,0,0},
{0,0,0,1},
{0,0,0,0},
{0,1,0,0},
{0,0,0,0},
{0,0,1,0},
{1,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,1},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,1,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,1,0},
{1,0,0,0},
{0,0,0,0},
{0,0,0,1},
{0,0,0,0},
{0,1,0,0},
{0,0,0,0},
{0,0,1,0},
{0,0,0,0},
{1,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,1,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,1,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,1},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,1,0},
{0,0,0,0},
{1,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,1},
{0,1,0,0},
{0,0,0,0},
{0,0,2,0},
{0,0,0,0},
{1,0,0,0},
{0,0,2,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,1,0,0},
{0,0,0,0},
{0,0,0,1},
{0,0,0,0},
{1,0,0,0},
{0,0,1,0},
{0,0,0,0},
{0,1,0,0},
{0,0,0,0},
{0,0,0,1},
{0,0,0,0},
{0,0,2,0},
{0,0,0,1},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{1,0,0,0},
{0,0,0,0},
{0,0,2,0},
{0,0,0,0},
{0,0,0,0},
{0,1,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,1},
{0,0,0,0},
{0,0,0,0},
{0,0,1,0},
{0,0,0,0},
{0,1,0,0},
{0,0,0,0},
{0,1,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{1,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,1,1,0},
{0,0,0,0},
{0,0,0,0},
{1,0,0,1},
{0,0,0,0},
{0,0,0,0},
{0,1,1,0},
{0,0,0,0},
{0,0,1,1},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{1,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,1,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,1},
{0,0,0,0},
{0,0,1,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{1,0,0,0},
{0,0,1,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,1,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,1},
{0,0,0,0},
{0,0,0,0},
{0,0,1,0},
{0,0,0,0},
{0,0,0,0},
{1,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,1},
{0,1,0,0},
{0,0,2,0},
{1,0,0,0},
{0,0,0,0},
{0,0,0,1},
{0,0,0,0},
{0,1,2,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0},
{0,0,0,0}
    };

    public Slider musicSlider;

    public Text bpmNowText;

    public bool OnPlay = false;

    public List<EpicBeat> allEpicBeats;

    public GameObject epicBeatPrefab;

    public GameObject allSongBeats;


    void Start()
    {
        //Load the AudioSource attached to the Conductor GameObject
        musicSource = GetComponent<AudioSource>();

        musicLength = musicSource.clip.length;

        totalBeats = (Mathf.RoundToInt((songBpm * musicLength) / 60));

        //notes = new int[totalBeats, 4];


        //Calculate the number of seconds in each beat
        secPerBeat = 60f / songBpm;

        beatPerSec = 1 / secPerBeat;

        //Record the time when the music starts
        dspSongTime = (float)AudioSettings.dspTime;

        //musicSource.Play();

        musicSlider.minValue = 0;
        musicSlider.maxValue = totalBeats;

        bpmNowText.text = (musicSlider.value).ToString();

        SpawnAllEpicBeat();
    }

    void Update()
    {
        if (OnPlay)
        {
            time = musicSource.time;

            //determine how many seconds since the song started
            songPosition = (float)(AudioSettings.dspTime - dspSongTime);

            //determine how many beats since the song started
            songPositionInBeats = time / secPerBeat;

            beatNow = (int)songPositionInBeats;

            musicSlider.value = beatNow;

            bpmNowText.text = (musicSlider.value).ToString();

            allSongBeats.transform.Translate(0, -beatPerSec * Time.deltaTime * musicSource.pitch, 0);
        }
        else
        {
            allSongBeats.transform.position = new Vector3(allSongBeats.transform.position.x, -beatNow, allSongBeats.transform.position.z);
        }

        if (beatNow == totalBeats)
        {
            OnPlay = false;
        }

        record(beatNow);
    }

    #region FUNC:SpawnAllEpicBeat
    void SpawnAllEpicBeat()
    {
        for(int i = 0; i < totalBeats; i++)
        {
            GameObject obj = Instantiate(epicBeatPrefab, allSongBeats.transform.position + new Vector3(0,i,0), allSongBeats.transform.rotation);
            obj.transform.SetParent(allSongBeats.transform);

            EpicBeat beat = obj.GetComponent<EpicBeat>();

            for(int j = 0; j < 4; j++)
            {
                if(notes[i,j] == 1)
                {
                    beat.notes[j].SetActive(true);
                    beat.longNotes[j].SetActive(false);
                }
                if (notes[i, j] == 2)
                {
                    beat.notes[j].SetActive(false);
                    beat.longNotes[j].SetActive(true);
                }
            }

            allEpicBeats.Add(beat);
        }
    }
    #endregion

    void record(int bpm)
    {
        if (bpm == totalBeats) return;
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (notes[bpm, 0] == 0)
            {
                notes[bpm, 0] = 1;
                allEpicBeats[bpm].notes[0].SetActive(true);
                allEpicBeats[bpm].longNotes[0].SetActive(false);
            }
            else if (notes[bpm, 0] == 1)
            {
                notes[bpm, 0] = 2;
                allEpicBeats[bpm].notes[0].SetActive(false);
                allEpicBeats[bpm].longNotes[0].SetActive(true);
            }
            else if (notes[bpm, 0] == 2)
            {
                notes[bpm, 0] = 0;
                allEpicBeats[bpm].notes[0].SetActive(false);
                allEpicBeats[bpm].longNotes[0].SetActive(false);
            }
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (notes[bpm, 1] == 0)
            {
                notes[bpm, 1] = 1;
                allEpicBeats[bpm].notes[1].SetActive(true);
                allEpicBeats[bpm].longNotes[1].SetActive(false);
            }
            else if (notes[bpm, 1] == 1)
            {
                notes[bpm, 1] = 2;
                allEpicBeats[bpm].notes[1].SetActive(true);
                allEpicBeats[bpm].longNotes[1].SetActive(true);
            }
            else if (notes[bpm, 1] == 2)
            {
                notes[bpm, 1] = 0;
                allEpicBeats[bpm].notes[1].SetActive(false);
                allEpicBeats[bpm].longNotes[1].SetActive(false);
            }
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (notes[bpm, 2] == 0)
            {
                notes[bpm, 2] = 1;
                allEpicBeats[bpm].notes[2].SetActive(true);
                allEpicBeats[bpm].longNotes[2].SetActive(false);
            }
            else if (notes[bpm, 2] == 1)
            {
                notes[bpm, 2] = 2;
                allEpicBeats[bpm].notes[2].SetActive(true);
                allEpicBeats[bpm].longNotes[2].SetActive(true);
            }
            else if (notes[bpm, 2] == 2)
            {
                notes[bpm, 2] = 0;
                allEpicBeats[bpm].notes[2].SetActive(false);
                allEpicBeats[bpm].longNotes[2].SetActive(false);
            }
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            if (notes[bpm, 3] == 0)
            {
                notes[bpm, 3] = 1;
                allEpicBeats[bpm].notes[3].SetActive(true);
                allEpicBeats[bpm].longNotes[3].SetActive(false);
            }
            else if (notes[bpm, 3] == 1)
            {
                notes[bpm, 3] = 2;
                allEpicBeats[bpm].notes[3].SetActive(false);
                allEpicBeats[bpm].longNotes[3].SetActive(true);
            }
            else if (notes[bpm, 3] == 2)
            {
                notes[bpm, 3] = 0;
                allEpicBeats[bpm].notes[3].SetActive(false);
                allEpicBeats[bpm].longNotes[3].SetActive(false);
            }
        }



    }

    public void OnSliderValueChanged()
    {
        beatNow = (int)(musicSlider.value);
        bpmNowText.text = (musicSlider.value).ToString();
        
    }

    public void OnNextBtnPressed()
    {
        musicSlider.value++;
    }

    public void OnPreviousBtnPressed()
    {
        musicSlider.value--;
    }

    public void OnPlayPauseBtnPressed()
    {
        if (!OnPlay)
        {
            songPositionInBeats = musicSlider.value;
            musicSource.time = songPositionInBeats * secPerBeat;
            //Start the music
            musicSource.Play();
            OnPlay = true;
        }
        else
        {
            //Stop the music
            musicSource.Pause();
            OnPlay = false;
        }
        
    }

    public void OnSaveBtnPressed()
    {
        string saveStr = "";
        for (int i = 0; i < totalBeats; i++)
        {
            saveStr += "{";
            for (int j = 0; j <= 3; j++)
            {
                if (j == 3)
                    saveStr += notes[i, j].ToString();
                else
                    saveStr += notes[i, j].ToString() + ",";
            }
            if (i == totalBeats - 1)
                saveStr += "} \n";
            else
                saveStr += "}, \n";
        }
        Debug.Log(saveStr);
    }


}
