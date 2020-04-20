using System.Collections;
using TMPro;
using UnityEngine;

public class PageControl : MonoBehaviour
{
    public PageControl nextPage;
    public IntroControl introControl;
    public float characterSpeed = .02f;
    public AudioSource sfx;

    TextMeshProUGUI[] sentences;
    int index = 0;
    string cachedText;
    Coroutine currentCoroutine;

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
        currentCoroutine = StartCoroutine(ShowSentence());
    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            if (currentCoroutine == null)
            {
                currentCoroutine = StartCoroutine(ShowSentence());
            }
            else
            {
                LineEnd();
            }
        }
    }

    void LineEnd()
    {
        StopCoroutine(currentCoroutine);
        sentences[index].text = cachedText; 
        currentCoroutine = null;
        index++;
    }

    IEnumerator ShowSentence()
    {
        Debug.Log($"[{name}] sentence count: {sentences.Length}, index: {index}");
        if (index >= sentences.Length)
        {
            if (!nextPage)
            {
                introControl.Next();
                gameObject.SetActive(false);
                yield break;
            }
            nextPage.gameObject.SetActive(true);
            gameObject.SetActive(false);
            yield break;
        }

        var textControl = sentences[index];

        cachedText = textControl.text;
        textControl.text = "";
        textControl.gameObject.SetActive(true);

        for (var i = 1; i <= cachedText.Length; ++i)
        {
            textControl.text = cachedText.Substring(0, i);
            sfx.Play();
            yield return new WaitForSeconds(characterSpeed);
        }
        index++;
        currentCoroutine = null;
    }
}
