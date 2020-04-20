using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FightingGame : MonoBehaviour
{
    public Animator animator;
    public SequenceControl sequenceControl;
    public Countdown countdown;
    public DialogueControl dialogueControl;
    public AudioSource audioSource;

    public MengNanValue mengNanValue;
    public int successScore = 20;
    public int loseScore = 20;

    public AudioClip winSFX;
    public AudioClip loseSFX;

    public TextAsset blameText;

    List<string> blames;

    void Awake()
    {
        blames = JsonUtility.FromJson<BlameContainer>(blameText.text).blames;
    }

    string Pick()
    {
        return blames[Random.Range(0, blames.Count)];
    }

    public void RoundStart()
    {
        dialogueControl.gameObject.SetActive(true);
        dialogueControl.ShowText(Pick());
        countdown.StartCountdown();
        sequenceControl.CreateSequence();
    }

    public void RoundWin()
    {
        dialogueControl.gameObject.SetActive(false);
        countdown.Terminate();
        animator.SetTrigger("win");
        audioSource.clip = winSFX;
        audioSource.Play();
        StartCoroutine(WaitAndFireNextRound());
        
        mengNanValue.incr_mengnan_value(successScore);
    }

    public void RoundLose()
    {
        sequenceControl.Clear();
        animator.SetTrigger("lose");
        countdown.Terminate();
        audioSource.clip = loseSFX;
        audioSource.Play();
        StartCoroutine(WaitAndFireNextRound());

        mengNanValue.incr_mengnan_value(-loseScore);
    }
    
    IEnumerator WaitAndFireNextRound()
    {
        yield return new WaitForSeconds(1f);
        RoundStart();
    }
}

[Serializable]
public class BlameContainer
{
    public List<string> blames;
}