using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

using UnityEngine.Networking;

public class SongMenu : MonoBehaviour
{
    [SerializeField]
    List<SongData> songDatas = new List<SongData>();

    [SerializeField]
    private GameObject contentObj;

    [SerializeField]
    private GameObject songListPanelPrefab;

    [SerializeField]
    private List<Button> buttons;

    AudioSource audioSource;
    AudioClip audioClip;

    [SerializeField]
    private Text bestScoreTxt;

    [SerializeField]
    private Text bestComboTxt;

    // Start is called before the first frame update
    void Start()
    {
        LoadFromJsons();

        InitMenu();

        audioSource = GetComponent<AudioSource>();

        StartCoroutine(LoadAudio());


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene("GamePlayScene");
        }
    }

    void LoadFromJsons()
    {
        string path = Path.Combine(Application.dataPath, "SongDatas");

        DirectoryInfo info = new DirectoryInfo(path);

        DirectoryInfo[] folders = info.GetDirectories();

        foreach(DirectoryInfo folder in folders)
        {
            FileInfo[] fileInfos = folder.GetFiles("*.txt");

            foreach (FileInfo jsonFile in fileInfos)
            {
                string loadData = File.ReadAllText(jsonFile.FullName);

                SongData data = JsonUtility.FromJson<SongData>(loadData);
                songDatas.Add(data);
            }
        }
        
    }

    void InitMenu()
    {
        for(int i = 0; i < songDatas.Count; i++)
        {
            GameObject obj = Instantiate(songListPanelPrefab, contentObj.transform);
            obj.transform.SetParent(contentObj.transform);

            obj.GetComponent<SongListPanel>().songNameTxt.text = songDatas[i].songName;

            // pass value v.s pass reference
            int copy = i;
            obj.GetComponent<Button>().onClick.AddListener(delegate () { OnBtnClicked(copy); });
            buttons.Add(obj.GetComponent<Button>());
        }

        bestScoreTxt.text = songDatas[0].highScore.ToString();
        bestComboTxt.text = songDatas[0].highCombo.ToString();
        buttons[GameData.selectedPanelIndex].transform.localScale = new Vector3(1.2f, 1.2f, 0);


    }

    private IEnumerator LoadAudio()
    {
        AudioClip myClip = null;

        string path = Path.Combine(Application.dataPath, "SongDatas");

        DirectoryInfo info = new DirectoryInfo(path);

        DirectoryInfo[] folders = info.GetDirectories();

        path = Path.Combine(folders[GameData.selectedPanelIndex].FullName, "music.mp3");

        Debug.Log(path);

        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(path, AudioType.MPEG))
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

        PlayAudioFile();
    }

    private void PlayAudioFile()
    {
        audioSource.clip = audioClip;
        audioSource.Play();
        audioSource.loop = true;
    }

    

    private UnityWebRequest GetAudioFromFile(int index)
    {
        string path = Path.Combine(Application.dataPath, "SongDatas");

        DirectoryInfo info = new DirectoryInfo(path);

        DirectoryInfo[] folders = info.GetDirectories();

        path = Path.Combine(folders[index].FullName, "music.mp3");
        
        UnityWebRequest request = new UnityWebRequest(path);
        return request;
    }

    public void OnBtnClicked(int index)
    {
        buttons[GameData.selectedPanelIndex].transform.localScale = new Vector3(1.0f, 1.0f, 0);
        GameData.selectedPanelIndex = index;
        buttons[GameData.selectedPanelIndex].transform.localScale = new Vector3(1.2f, 1.2f, 0);
        bestScoreTxt.text = songDatas[GameData.selectedPanelIndex].highScore.ToString();
        bestComboTxt.text = songDatas[GameData.selectedPanelIndex].highCombo.ToString();

        StartCoroutine(LoadAudio());
    }

}
