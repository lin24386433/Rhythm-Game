using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConcludeManager : MonoBehaviour
{
    [SerializeField]
    private Text perfectTxt;

    [SerializeField]
    private Text goodTxt;

    [SerializeField]
    private Text badTxt;

    [SerializeField]
    private Text missTxt;

    [SerializeField]
    private Text comboTxt;

    [SerializeField]
    private Text maxCpmboTxt;

    [SerializeField]
    private Text scoreTxt;

    [SerializeField]
    private Text bestScoreTxt;

    private void Awake()
    {
        perfectTxt.text = "Perfect\n" + GameData.perfectCount.ToString();
        goodTxt.text = "Good\n" + GameData.goodCount.ToString();
        badTxt.text = "Bad\n" + GameData.badCount.ToString();
        missTxt.text = "Miss\n" + GameData.missCount.ToString();

        scoreTxt.text = GameData.score.ToString();

        comboTxt.text = "Combo:" + GameData.maxCombo.ToString() + "/284";


    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
