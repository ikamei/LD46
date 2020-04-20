using DG.Tweening;
using TMPro;
using UnityEngine;

public class PageControl : MonoBehaviour
{
    public PageControl nextPage;
    public IntroControl introControl;
    public float sentenceFadeTime = .5f;

    TextMeshProUGUI[] sentences;
    int index = 0;

    void Awake()
    {
        sentences = GetComponentsInChildren<TextMeshProUGUI>();
    }

    void OnEnable()
    {
        foreach (var sentence in sentences)
        {
            sentence.gameObject.SetActive(false);
        }
        ShowSentence();
    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            Debug.Log("any key");
            ShowSentence();
        }
    }

    void ShowSentence()
    {
        Debug.Log($"[{name}] sentence count: {sentences.Length}, index: {index}");
        if (index >= sentences.Length)
        {
            if (!nextPage)
            {
                introControl.Next();
                gameObject.SetActive(false);
                return;
            }
            nextPage.gameObject.SetActive(true);
            gameObject.SetActive(false);
            return;
        }

        var textControl = sentences[index];
        textControl.gameObject.SetActive(true);
        textControl.DOFade(1f, sentenceFadeTime).From(0);
        index++;
    }
}
