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
            var displayText = text.Substring(0, i);
            if (i < text.Length)
            {
                var lastCharacter = text.Substring(i, 1);
                // Debug.Log($"last character: {lastCharacter}, {lastCharacter.Trim().Length}");
                if (lastCharacter.Trim().Length != 0)
                {
                    audioSource.Play();
                }
                else
                {
                    //Debug.Log("word break");
                }
            }
            textControl.text = displayText;
            yield return new WaitForSeconds(characterSpeed);
        }
        currentCoroutine = null;
        textCompleted.Invoke();
        idle = true;
    }
}
