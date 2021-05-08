using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class SongMenu : MonoBehaviour
{
    [SerializeField]
    List<SongData> songDatas = new List<SongData>();

    [SerializeField]
    private GameObject contentObj;

    [SerializeField]
    private GameObject songListPanelPrefab;

    private int selectedPanelIndex = 0;

    [SerializeField]
    private List<Button> buttons;

    [SerializeField]
    private Text bestScoreTxt;

    [SerializeField]
    private Text bestComboTxt;

    // Start is called before the first frame update
    void Start()
    {
        LoadFromJsons();

        InitMenu();



        for(int i = 0; i < buttons.Count; i++)
        {
            int copy = i;
            buttons[i].onClick.AddListener(delegate () { OnBtnClicked(copy);  });
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LoadFromJsons()
    {
        string path = Path.Combine(Application.dataPath, "SongDatas");

        DirectoryInfo info = new DirectoryInfo(path);

        FileInfo[] fileInfos = info.GetFiles("*.txt");

        foreach (FileInfo jsonFile in fileInfos)
        {
            string loadData = File.ReadAllText(jsonFile.FullName);

            SongData data = JsonUtility.FromJson<SongData>(loadData);
            songDatas.Add(data);
        }
    }

    void InitMenu()
    {
        for(int i = 0; i < songDatas.Count; i++)
        {
            GameObject obj = Instantiate(songListPanelPrefab, contentObj.transform);
            obj.transform.SetParent(contentObj.transform);

            obj.GetComponent<SongListPanel>().songNameTxt.text = songDatas[i].songName;
            buttons.Add(obj.GetComponent<Button>());
        }
        bestScoreTxt.text = songDatas[0].highScore.ToString();
        bestComboTxt.text = songDatas[0].highCombo.ToString();
        buttons[selectedPanelIndex].transform.localScale = new Vector3(1.2f, 1.2f, 0);
    }

    public void OnBtnClicked(int index)
    {
        buttons[selectedPanelIndex].transform.localScale = new Vector3(1.0f, 1.0f, 0);
        selectedPanelIndex = index;
        buttons[selectedPanelIndex].transform.localScale = new Vector3(1.2f, 1.2f, 0);
        bestScoreTxt.text = songDatas[selectedPanelIndex].highScore.ToString();
        bestComboTxt.text = songDatas[selectedPanelIndex].highCombo.ToString();
    }

}
