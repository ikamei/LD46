using System.Collections;
using UnityEngine;

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

    public void RoundStart()
    {
        dialogueControl.gameObject.SetActive(true);
        dialogueControl.ShowText("blah blah blah ...");
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