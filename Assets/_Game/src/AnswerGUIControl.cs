using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AnswerGUIControl : MonoBehaviour
{
    public TextMeshProUGUI answerText;
    public Image costImage;

    public Sprite spr5;
    public Sprite spr10;
    public Sprite spr25;
    public Sprite spr50;

    Dictionary<int, Sprite> costMapping;

    void Awake()
    {
        costMapping = new Dictionary<int, Sprite>
        {
            [5] = spr5, 
            [10] = spr10, 
            [25] = spr25, 
            [50] = spr50
        };
    }

    public void UpdateText(string answer, int cost)
    {
        answerText.text = answer;
        costImage.sprite = costMapping[cost];
        var color = costImage.color;
        color.a = 1;
        costImage.color = color;
    }

    public void Clear()
    {
        answerText.text = "";
        var color = costImage.color;
        color.a = 0;
        costImage.color = color;
    }
}
