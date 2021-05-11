using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SongListPanel : MonoBehaviour
{
    public songDifficulty difficulty = songDifficulty.easy;

    public Text songNameTxt;

    public Image songRankImg;

    [SerializeField]
    Sprite[] imgs;

    [SerializeField]
    Sprite[] selectedStyle;

    private void Start()
    {
        Image img = GetComponent<Image>();

        switch (difficulty)
        {
            case songDifficulty.easy:
                img.sprite = imgs[0];
            break;
            case songDifficulty.normal:
                img.sprite = imgs[1];
            break;
            case songDifficulty.hard:
                img.sprite = imgs[2];
            break;
            case songDifficulty.expert:
                img.sprite = imgs[3];
            break;
            case songDifficulty.master:
                img.sprite = imgs[4];
            break;
        }

    }

    public void ChangeSelectedStyle(bool selected)
    {
        if (selected)
        {
            switch (difficulty)
            {
                case songDifficulty.easy:
                    GetComponent<Image>().sprite = imgs[0];
                    break;
                case songDifficulty.normal:
                    GetComponent<Image>().sprite = imgs[1];
                    break;
                case songDifficulty.hard:
                    GetComponent<Image>().sprite = imgs[2];
                    break;
                case songDifficulty.expert:
                    GetComponent<Image>().sprite = imgs[3];
                    break;
                case songDifficulty.master:
                    GetComponent<Image>().sprite = imgs[4];
                    break;
            }
        }
        else
        {
            switch (difficulty)
            {
                case songDifficulty.easy:
                    this.GetComponent<Image>().sprite = selectedStyle[0];
                    break;
                case songDifficulty.normal:
                    this.GetComponent<Image>().sprite = selectedStyle[1];
                    break;
                case songDifficulty.hard:
                    this.GetComponent<Image>().sprite = selectedStyle[2];
                    break;
                case songDifficulty.expert:
                    this.GetComponent<Image>().sprite = selectedStyle[3];
                    break;
                case songDifficulty.master:
                    this.GetComponent<Image>().sprite = selectedStyle[4];
                    break;
            }
            
        }
            
    }

}
