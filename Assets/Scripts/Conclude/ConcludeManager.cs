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
    private Text bestComboTxt;

    [SerializeField]
    private Text scoreTxt;

    [SerializeField]
    private Text bestScoreTxt;

    private float accurate;

    [SerializeField]
    Sprite[] imgs;

    private void Awake()
    {
        perfectTxt.text = "Perfect\n" + GameData.perfectCount.ToString();
        goodTxt.text = "Good\n" + GameData.goodCount.ToString();
        badTxt.text = "Bad\n" + GameData.badCount.ToString();
        missTxt.text = "Miss\n" + GameData.missCount.ToString();

        scoreTxt.text = GameData.score.ToString();

        comboTxt.text = "Combo:" + GameData.maxCombo.ToString() + "/" + GameData.totalCombo;
        bestComboTxt.text = "Best:" + GameData.highCombo.ToString();

        bestScoreTxt.text = "Best:" + GameData.highScore.ToString();

        Image img = GetComponent<Image>();

        accurate = (float)(GameData.score / GameData.totalScore) * 100;

        if(accurate >= 96)
        {
            img.sprite = imgs[0];   // S
        }
        else if (accurate >= 90)
        {
            img.sprite = imgs[1];   // A
        }
        else if (accurate >= 80)
        {
            img.sprite = imgs[2];   // B
        }else if (accurate >= 70)
        {
            img.sprite = imgs[3];   // C
        }else
        {
            img.sprite = imgs[4];   // F
        }

    }
}
