using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.Networking;

public class GameController : MonoBehaviour
{
    public SongData dataToSave;

    public SongData loadedData;

    // Basic Setup
    #region BasicVariablesWithJson
    [Header("Basic Song Data")]
    public string songName;

    public int songBPM;

    public float songLength;

    public int[,] songNotes;

    public int songDifficulty;

    public int totalCombo;

    public int totalScore;

    // Player data
    public int highCombo;

    public int highScore;

    public int playTimes;

    #endregion

    // �Э��]�w
    public int[,] notes;
    /*
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
    */

    /// <summary>
    /// �C����¦�ܼ�
    /// </summary>
    #region GameBasicVariables
    [Header("Game Basic Data")]
    // GameController ���
    public static GameController instance;

    // ����Prefab
    public GameObject notePrefab;

    public GameObject longNotePrefab;

    // 4�ӧP�w�I
    public GameObject[] detectPoints;

    // �C�����Ƥ�combo��
    public int gameScore = 0;
    public int combo = 0;

    public int fullComboNumber;

    // �C��UI
    public Text scoreTxt;
    public Text comboTxt;

    #endregion

    /// <summary>
    /// ���֬����ܼ�
    /// </summary>
    #region MusicVariables
    [Header("Music Data")]
    // ���ֶ}�l�e���ݮɶ�
    public float timeBeforeStart;

    // ����BPM
    public float songBpm;

    // �`Beat��
    public int totalBeats;

    // �@��X��Beat
    public float secPerBeat;

    // �@��Beat�X��
    public float beatPerSec;

    // Current song position, in seconds
    // 
    public float songPosition;

    // Current song position, in beats
    // ���ּ����ĴX��BPM
    public float songPositionInBeats;

    public int beatNow;

    // How many seconds have passed since the song started
    public float dspSongTime;

    // an AudioSource attached to this GameObject that will play the music.
    AudioSource audioSource;
    AudioClip audioClip;

    // ���֪���
    public float musicLength;

    #endregion

    public Text songNameTxt;

    private void Awake()
    {
        SongLoadedFromJson();

        notes = StringToTwoDimensionalArray(loadedData.songNotesStrVer);

        songBpm = loadedData.songBPM;

        // ��l�ƭ��ֳ]�w
        MusicSetUP();

        BasicSetUP();

    }

    private void Start()
    {

    }

    void Update()
    {
        // ���ְѼƧ�s
        MusicUpdate();

        // �ͦ��Э�
        SpawnNotes();

        // ��sUI
        UIUpdate();

        ScoreUpdate();

    }

    /// <summary>
    /// 1. ���oAudioSource
    /// 2. �`�@�X��Beats
    /// 3. �C��X��Beat�B�CBeat�X��
    /// 4. �p��í˼Ƶ��ݮɶ����񭵼�
    /// </summary>
    #region FUNC:MusicSetUP, BasicSetUP
    void MusicSetUP()
    {
        //Load the AudioSource attached to the Conductor GameObject
        audioSource = GetComponent<AudioSource>();

        musicLength = audioSource.clip.length;

        totalBeats = (Mathf.RoundToInt((songBpm * musicLength) / 60));

        //Calculate the number of seconds in each beat
        secPerBeat = 60f / songBpm;

        beatPerSec = songBpm / 60f;

        //Record the time when the music starts
        dspSongTime = (float)AudioSettings.dspTime;

        timeBeforeStart = (((10 / secPerBeat) + 1) * secPerBeat) / beatPerSec;

        StartCoroutine(WaitForStart(timeBeforeStart));
    }

    void BasicSetUP()
    {
        if(instance == null)
            instance = this;
        fullComboNumber = calculateFullCombo();

        songNameTxt.text = loadedData.songName;
    }

    #endregion

    /// <summary>
    /// 1. ��s���ּ����ĴX��BBeat
    /// </summary>
    #region FUNC:MusicUpdate, UIUpdate
    void MusicUpdate()
    {
        //determine how many seconds since the song started
        songPosition = (float)(AudioSettings.dspTime - dspSongTime);

        //determine how many beats since the song started
        songPositionInBeats = songPosition / secPerBeat;

        beatNow = (int)songPositionInBeats;
    }

    void ScoreUpdate() 
    {
        GameData.score = gameScore;
        if(combo > GameData.maxCombo)
        {
            GameData.maxCombo = combo;
        }
    }


    /// <summary>
    /// 1. ��sUI
    /// </summary>
    void UIUpdate()
    {
        scoreTxt.text = gameScore.ToString();
        comboTxt.text = combo.ToString();
    }
    #endregion

    /// <summary>
    /// 1. �N�}�C����ഫ���Э��ͦ��b�̤W��
    /// </summary>
    #region FUNC:SpawnNotes
    void SpawnNotes()
    {
        for (int k = 0; k < totalBeats; k++)
        {
            if (beatNow == k)
            {
                for (int i = 0; i <= 3; i++)
                {
                    if (notes[k, i] == 1)
                    {
                        notes[k, i] = 0;
                        // ((10 / secPerBeat) + 1) * secPerBeat�O�C���e���~��secPerBeat���̤p���Ʀ�m
                        Instantiate(notePrefab, detectPoints[i].transform.position + new Vector3(0, ((10 / secPerBeat) + 1) * secPerBeat, 0), detectPoints[i].transform.rotation);
                    }
                    if (notes[k, i] == 2)
                    {
                        
                        int howManyBeatsForLongNote = 0;

                        notes[k, i] = 0;

                        for(int currentBeat = k; currentBeat < totalBeats; currentBeat++)
                        {
                            if (notes[currentBeat, i] == 2)
                            {
                                notes[currentBeat, i] = 0;
                                break;
                            }
                            howManyBeatsForLongNote++;
                        }


                        GameObject longNoteSpawned = Instantiate(longNotePrefab, detectPoints[i].transform.position + new Vector3(0, ((10 / secPerBeat) + 1) * secPerBeat, 0), detectPoints[i].transform.rotation);
                        longNoteSpawned.GetComponent<LongNote>().noteLength = howManyBeatsForLongNote * 1;
                        
                    }
                }
            }
        }
    }
    #endregion

    #region FUNC:calculateFullCombo
    int calculateFullCombo()
    {
        int x = 0;
        for (int k = 0; k < totalBeats; k++)
        {
            for (int i = 0; i <= 3; i++)
            {
                if (notes[k, i] == 1)
                {
                    x++;
                }
            }
        }
        return x;
    }
    #endregion

    #region FUNC:TwoDimensionalArrayToString, StringToTwoDimensionalArray
    public string TwoDimensionalArrayToString(int[,] array)
    {
        string saveStr = "";
        for (int i = 0; i < totalBeats; i++)
        {
            saveStr += "{";
            for (int j = 0; j <= 3; j++)
            {
                if (j == 3)
                    saveStr += array[i, j].ToString();
                else
                    saveStr += array[i, j].ToString() + ",";
            }
            if (i == totalBeats - 1)
                saveStr += "} \n";
            else
                saveStr += "}, \n";
        }
        return saveStr;
    }

    public int[,] StringToTwoDimensionalArray(string str)
    {
        int totalRows = 0;
        foreach (char ch in str)
        {
            if (ch == '}')
            {
                totalRows++;
            }
        }

        int[,] array = new int[totalRows, 4];
        int rowIndex = 0;       // 0~totalBeats-1
        int columnIndex = 0;    // 0~3

        foreach (char ch in str)
        {
            if(ch == '0')
            {
                array[rowIndex, columnIndex] = 0;
                columnIndex++;
            }
            else if (ch == '1')
            {
                array[rowIndex, columnIndex] = 1;
                columnIndex++;
            }
            else if (ch == '2')
            {
                array[rowIndex, columnIndex] = 2;
                columnIndex++;
            }
            else if (ch == '}')
            {
                rowIndex++;
                columnIndex = 0;
            }
        }

        return array;
    }

    #endregion

    #region FUNC: Load Frpm Json

    void SongLoadedFromJson()
    {
        string LoadData;

        string path = Path.Combine(Application.dataPath, "SongDatas");

        DirectoryInfo info = new DirectoryInfo(path);

        DirectoryInfo[] folders = info.GetDirectories();

        path = folders[GameData.selectedPanelIndex].FullName;

        path = Path.Combine(path, "Gurenge" + ".txt");

        LoadData = File.ReadAllText(path);

        //把字串轉換成Data物件
        loadedData = JsonUtility.FromJson<SongData>(LoadData);

        StartCoroutine(LoadAudio());
    }

    #endregion

    private IEnumerator LoadAudio()
    {
        AudioClip myClip = null;

        string path = Path.Combine(Application.dataPath, "SongDatas");

        DirectoryInfo info = new DirectoryInfo(path);

        DirectoryInfo[] folders = info.GetDirectories();

        path = Path.Combine(folders[GameData.selectedPanelIndex].FullName, "music.mp3");

#if UNITY_STANDALONE_OSX

        string url = "file://" + path;

#endif

#if UNITY_STANDALONE_LINUX

        string url = "file://" + path;

#endif

#if UNITY_STANDALONE_WIN

        string url = "file://" + path;

#endif

        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(url, AudioType.MPEG))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(www.error);
            }
            else
            {
                myClip = DownloadHandlerAudioClip.GetContent(www);
            }
        }

        audioClip = myClip;

        audioClip.name = "music";

        audioSource.clip = audioClip;
    }

    /// <summary>
    /// 1. �˼�timeToPlayMusic����񭵼�
    /// </summary>
    /// <param name="timeToPlayMusic"></param>
    /// <returns></returns>
    #region IEnumerator:WaitForStart
    IEnumerator WaitForStart(float timeToPlayMusic)
    {
        yield return new WaitForSeconds(timeToPlayMusic);
        // Start the music
        audioSource.Play();
    }
    #endregion
}

