using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum songDifficulty
{
    easy,
    normal,
    hard,
    expert,
    master
}

public class SongData
{
    public string songName;

    public int songBPM;

    public float songLength;

    public string songNotesStrVer;

    public songDifficulty songDifficulty = songDifficulty.easy;

    public int totalCombo;

    public int totalScore;

    public Image backGroundImage;

    public AudioClip audio;

    // Player data
    public int highCombo;

    public int highScore;

    public int playTimes;

}
