using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class GameController : MonoBehaviour
{
    public SongData dataToSave;

    public SongData loadedData;

    // Basic Setup
    #region BasicSetUPWithJson
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
    

    /// <summary>
    /// �C����¦�ܼ�
    /// </summary>
    #region GameBasicVariables

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
    public Text timingTxt;
    public Text scoreTxt;
    public Text comboTxt;

    #endregion

    /// <summary>
    /// ���֬����ܼ�
    /// </summary>
    #region MusicVariables

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
    public AudioSource musicSource;

    // ���֪���
    public float musicLength;

    #endregion

    private void Start()
    {
        SongLoadedFromJson();

        notes = StringToTwoDimensionalArray(loadedData.songNotesStrVer);

        Debug.Log(loadedData.songNotesStrVer);
 
        songBpm = loadedData.songBPM;

        // ��ҳ]�w
        instance = this;

        // ��l�ƭ��ֳ]�w
        MusicSetUP();

        BasicSetUP();

        //SongSaveToJson();

    }

    void Update()
    {
        // ���ְѼƧ�s
        MusicUpdate();

        // �ͦ��Э�
        SpawnNotes();

        // ��sUI
        UIUpdate();
    }

    /// <summary>
    /// 1. ���oAudioSource
    /// 2. �`�@�X��Beats
    /// 3. �C��X��Beat�B�CBeat�X��
    /// 4. �p��í˼Ƶ��ݮɶ����񭵼�
    /// </summary>
    void MusicSetUP()
    {
        //Load the AudioSource attached to the Conductor GameObject
        musicSource = GetComponent<AudioSource>();

        musicLength = musicSource.clip.length;

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
        fullComboNumber = calculateFullCombo();
    }

    /// <summary>
    /// 1. ��s���ּ����ĴX��BBeat
    /// </summary>
    void MusicUpdate()
    {
        //determine how many seconds since the song started
        songPosition = (float)(AudioSettings.dspTime - dspSongTime);

        //determine how many beats since the song started
        songPositionInBeats = songPosition / secPerBeat;

        beatNow = (int)songPositionInBeats;
    }

    /// <summary>
    /// 1. �N�}�C����ഫ���Э��ͦ��b�̤W��
    /// </summary>
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
                        notes[k, i] = 0;
                        Instantiate(longNotePrefab, detectPoints[i].transform.position + new Vector3(0, ((10 / secPerBeat) + 1) * secPerBeat, 0), detectPoints[i].transform.rotation);
                    }
                }
            }
        }
    }

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

    void SongSaveToJson()
    {
        dataToSave = new SongData();
        dataToSave.songName = "Gurenge";
        dataToSave.songBPM = 540;
        dataToSave.songNotesStrVer = TwoDimensionalArrayToString(notes);
        dataToSave.songLength = musicLength;
        dataToSave.songDifficulty = 5;
        dataToSave.totalCombo = fullComboNumber;
        dataToSave.totalScore = fullComboNumber * 500;
        dataToSave.highCombo = 0;
        dataToSave.highScore = 0;
        dataToSave.playTimes = 0;

        string jsonInfo = JsonUtility.ToJson(dataToSave, true);

        string path = Path.Combine(Application.dataPath, "data");
        //path = Path.Combine(path, "Gurenge.txt");

        File.WriteAllText(path, jsonInfo);


        Debug.Log("寫入完成");
        Debug.Log("dataPath: " + Application.dataPath);
    }

    void SongLoadedFromJson()
    {
        string LoadData;

        string path = Path.Combine(Application.dataPath, "data");

        LoadData = File.ReadAllText(path);

        //把字串轉換成Data物件
        loadedData = JsonUtility.FromJson<SongData>(LoadData);
        
    }

    /// <summary>
    /// 1. ��sUI
    /// </summary>
    void UIUpdate()
    {
        scoreTxt.text = "Score : " + gameScore;
        comboTxt.text = "Combo : " + combo;
    }

    /// <summary>
    /// 1. �˼�timeToPlayMusic����񭵼�
    /// </summary>
    /// <param name="timeToPlayMusic"></param>
    /// <returns></returns>
    IEnumerator WaitForStart(float timeToPlayMusic)
    {
        yield return new WaitForSeconds(timeToPlayMusic);
        // Start the music
        musicSource.Play();
    }
}

