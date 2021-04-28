using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Recorder : MonoBehaviour
{
    //Song beats per minute
    //This is determined by the song you're trying to sync up to
    public float songBpm;

    //The number of seconds for each song beat
    public float secPerBeat;

    //The number of beats for each second
    public float beatPerSec;

    //Current song position, in seconds
    public float songPosition;

    //Current song position, in beats
    public float songPositionInBeats;

    //How many seconds have passed since the song started
    public float dspSongTime;

    //an AudioSource attached to this GameObject that will play the music.
    public AudioSource musicSource;

    public float time;

    public float musicLength;

    public int beatNow;

    public int totalBeats;

    public Slider musicSlider;

    public Text bpmNowText;

    bool OnPlay = false;

    public GameObject[] notesObj;

    int[,] notes;

    private void Start()
    {

        //Load the AudioSource attached to the Conductor GameObject
        musicSource = GetComponent<AudioSource>();

        musicLength = musicSource.clip.length;

        totalBeats = (Mathf.RoundToInt((songBpm * musicLength) / 60));

        notes = new int[totalBeats, 4];


        //Calculate the number of seconds in each beat
        secPerBeat = 60f / songBpm;

        beatPerSec = 1 / secPerBeat;

        //Record the time when the music starts
        dspSongTime = (float)AudioSettings.dspTime;

        musicSlider.minValue = 0;
        musicSlider.maxValue = totalBeats;

        bpmNowText.text = (musicSlider.value).ToString();

        //StartCoroutine(waittime(secPerBeat));
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
        }
        if(beatNow == totalBeats)
        {
            OnPlay = false;
        }
        record((int)Mathf.Round(songPositionInBeats));
        ShowNotesInBeats(beatNow);
        
    }

    void record(int bpm)
    {
        if (bpm == totalBeats) return;
        if (Input.GetKeyDown(KeyCode.Z))
        {
            notes[bpm, 0] = notes[bpm, 0] == 1 ? 0 : 1;
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            notes[bpm, 1] = notes[bpm, 1] == 1 ? 0 : 1;
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            notes[bpm, 2] = notes[bpm, 2] == 1 ? 0 : 1;
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            notes[bpm, 3] = notes[bpm, 3] == 1 ? 0 : 1;
        }
    }

    void ShowNotesInBeats(int bpm)
    {
        if (bpm == totalBeats) return;
        for (int i = 0; i <= 3; i++)
        {
            if(notes[bpm,i] == 1)
            {
                notesObj[i].SetActive(true);
            }
            else
            {
                notesObj[i].SetActive(false);
            }
        }
    }

    public void OnPlayBtnPressed()
    {
        songPositionInBeats = musicSlider.value;
        musicSource.time = songPositionInBeats * secPerBeat;
        //Start the music
        musicSource.Play();
        OnPlay = true;
    }

    public void OnStopBtnPressed()
    {
        //Stop the music
        musicSource.Pause();
        OnPlay = false;
    }

    public void OnSaveBtnPressed()
    {
        string saveStr = "";
        for(int i = 0; i< totalBeats; i++)
        {
            saveStr += "{";
            for (int j = 0; j <= 3; j++)
            {
                if(j==3)
                    saveStr += notes[i,j].ToString();
                else
                    saveStr += notes[i, j].ToString() + ",";
            }
            if(i== totalBeats-1)
                saveStr += "} \n";
            else
                saveStr += "}, \n";
        }
        Debug.Log(saveStr);
    }

    public void OnSliderValueChanged()
    {
        beatNow = (int)(musicSlider.value);
        bpmNowText.text = (musicSlider.value).ToString();
        ShowNotesInBeats(Mathf.RoundToInt(songPositionInBeats));
    }

    IEnumerator waittime(float mytime)
    {
        for (int i = 0; i <= 19; i++)
        {
            yield return new WaitForSeconds(mytime); // ����x��
        }
    }
}
