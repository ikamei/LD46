using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class DialogueControl : MonoBehaviour
{
    public TextMeshProUGUI textControl;
    public AudioSource audioSource;
    public float characterSpeed = .05f;
    public UnityEvent textCompleted;

    public bool idle;

    Coroutine currentCoroutine;

    void OnGUI()
    {
        if (GUILayout.Button("show text"))
            ShowText("hello world");
    }

    public void ShowText(string text)
    {
        if (currentCoroutine != null) StopCoroutine(currentCoroutine);
        currentCoroutine = StartCoroutine(DisplayCharacter(text));
    }

    IEnumerator DisplayCharacter(string text)
    {
        idle = false;
        for (var i = 0; i < text.Length + 1; ++i)
        {
            if (!text.EndsWith(" "))
            {
                Debug.Log("word break");
                audioSource.Play();
            }

            textControl.text = text.Substring(0, i);
            yield return new WaitForSeconds(characterSpeed);
        }
        currentCoroutine = null;
        textCompleted.Invoke();
        idle = true;
    }
}
