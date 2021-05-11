using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public SongData dataToSave;

    public SongData loadedData;

    // Game Notes
    public int[,] notes;

    #region GameBasicVariables
    [Header("Game Basic Data")]
    // GameController instance
    public static GameController instance;

    // Note Prefab
    public GameObject notePrefab;

    // Long Note Prefab
    public GameObject longNotePrefab;

    // Detecters
    public GameObject[] detectPoints;

    // Game Score & Combo
    public int gameScore = 0;
    public int combo = 0;

    // Game UI
    public Text scoreTxt;
    public Text comboTxt;

    #endregion


    #region MusicVariables
    [Header("Music Data")]
    // Wait Time before note falling down
    public float timeBeforeStart;

    // Song BPM
    public float songBpm;

    // Total Beats in notes
    public int totalBeats;

    // how many beats in one second
    public float secPerBeat;

    // how many second in a beat
    public float beatPerSec;

    // Current song position, in seconds
    public float songPosition;

    // Current song position, in beats, float ver
    public float songPositionInBeats;

    // Current song position, in beats, int ver
    public int beatNow;

    // How many seconds have passed since the song started
    public float dspSongTime;

    // an AudioSource attached to this GameObject that will play the music.
    AudioSource audioSource;
    AudioClip audioClip;

    // music's length
    public float musicLength;

    #endregion

    public Text songNameTxt;

    private void Awake()
    {
        SongLoadedFromJson();

        StartCoroutine(LoadAudio());

        notes = StringToTwoDimensionalArray(loadedData.songNotesStrVer);

        songBpm = loadedData.songBPM;

        GameData.highCombo = loadedData.highCombo;

        GameData.highScore = loadedData.highScore;

        GameData.totalCombo = loadedData.totalCombo;

        GameData.totalScore = loadedData.totalScore;

        MusicSetUP();

        BasicSetUP();

    }

    private void Start()
    {

    }

    void Update()
    {
        MusicUpdate();

        SpawnNotes();

        UIUpdate();

        ScoreUpdate();

    }

    #region FUNC:MusicSetUP, BasicSetUP
    void MusicSetUP()
    {
        // Load the AudioSource attached to the Conductor GameObject
        audioSource = GetComponent<AudioSource>();

        musicLength = audioSource.clip.length;

        totalBeats = (Mathf.RoundToInt((songBpm * musicLength) / 60));

        // Calculate the number of seconds in each beat
        secPerBeat = 60f / songBpm;

        beatPerSec = songBpm / 60f;

        // Record the time when the music starts
        dspSongTime = (float)AudioSettings.dspTime;

        timeBeforeStart = (((10 / secPerBeat) + 1) * secPerBeat) / beatPerSec;

        StartCoroutine(WaitForStart(timeBeforeStart));
    }

    void BasicSetUP()
    {
        if(instance == null)
            instance = this;

        songNameTxt.text = loadedData.songName;
    }

    #endregion

    #region FUNC:MusicUpdate, UIUpdate
    void MusicUpdate()
    {
        // determine how many seconds since the song started
        songPosition = (float)(AudioSettings.dspTime - dspSongTime);

        // determine how many beats since the song started
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

        if (beatNow == totalBeats)
        {
            StartCoroutine(WaitForEnd(3f));
        }

    }

    void UIUpdate()
    {
        scoreTxt.text = gameScore.ToString();
        comboTxt.text = combo.ToString();
    }
    #endregion

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
                        // ((10 / secPerBeat) + 1) * secPerBeat to determine where to spawn ouside bounds
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

        
    }

    void SongSaveToJson()
    {
        dataToSave = loadedData;
        dataToSave.songName = "Gurenge";
        dataToSave.songBPM = 540;
        dataToSave.songLength = musicLength;
        dataToSave.songDifficulty = songDifficulty.easy;

        if (GameData.maxCombo >= GameData.highCombo)
        {
            dataToSave.highCombo = GameData.maxCombo;
            GameData.highCombo = GameData.maxCombo;
        }
            
        if (gameScore >= GameData.highScore)
        {
            dataToSave.highScore = gameScore;
            GameData.highScore = gameScore;
        }
            
        dataToSave.playTimes = loadedData.playTimes++;

        string jsonInfo = JsonUtility.ToJson(dataToSave, true);

        string path = Path.Combine(Application.dataPath, "SongDatas");

        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        path = Path.Combine(path, dataToSave.songName);

        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        path = Path.Combine(path, dataToSave.songName + ".txt");

        File.WriteAllText(path, jsonInfo);

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

    #region IEnumerator:WaitForStart, WaitForEnd
    IEnumerator WaitForStart(float timeToPlayMusic)
    {
        yield return new WaitForSeconds(timeToPlayMusic);
        // Start the music
        audioSource.Play();
    }

    IEnumerator WaitForEnd(float time)
    {
        yield return new WaitForSeconds(time);
        SongSaveToJson();
        SceneManager.LoadScene("ConcludeScene");
    }
    #endregion
}

