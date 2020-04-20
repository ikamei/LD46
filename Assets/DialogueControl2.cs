using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class DialogueControl2 : MonoBehaviour
{
    public string text;
    public TextMeshProUGUI textControl;
    
    public AudioSource audioSource;
    public float characterSpeed = .05f;
    public UnityEvent onIdle;

    DialogueState state;
    enum DialogueState
    {
        Playing,
        Idle
    }

    Coroutine currentCoroutine;

    void Awake()
    {
        state = DialogueState.Idle;
    }

    void Update()
    {
        switch (state)
        {
        case DialogueState.Idle:
            break;
        case DialogueState.Playing:
            if (Input.anyKey)
            {
                EndDialogue();
            }
            break;
        }
    }

    void OnEnable()
    {
        StartDialogue();
    }

    void OnValidate()
    {
        textControl.text = text;
    }

    void StartDialogue()
    {
        if (currentCoroutine != null) StopCoroutine(currentCoroutine);
        currentCoroutine = StartCoroutine(DisplayCharacter());
    }
    
    void EndDialogue()
    {
        if (currentCoroutine != null) StopCoroutine(currentCoroutine);
        textControl.text = text;
        state = DialogueState.Idle;
        onIdle.Invoke();
    }

    IEnumerator DisplayCharacter()
    {
        state = DialogueState.Playing;
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
            }
            textControl.text = displayText;
            yield return new WaitForSeconds(characterSpeed);
        }
        currentCoroutine = null;

        state = DialogueState.Idle;
        onIdle.Invoke();
    }
}
