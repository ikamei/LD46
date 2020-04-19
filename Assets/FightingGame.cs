using System.Collections;
using UnityEngine;

public class FightingGame : MonoBehaviour
{
    public Animator animator;
    public SequenceControl sequenceControl;
    public Countdown countdown;
    public AudioSource audioSource;

    public AudioClip winSFX;
    public AudioClip loseSFX;
    
    void OnGUI()
    {
        if (GUILayout.Button("Game Start"))
        {
            RoundStart();
        }
    }

    public void RoundStart()
    {
        countdown.StartCountdown();
        sequenceControl.CreateSequence();
    }

    public void RoundWin()
    {
        countdown.Terminate();
        animator.SetTrigger("win");
        audioSource.clip = winSFX;
        audioSource.Play();
        StartCoroutine(WaitAndFireNextBank());
    }

    public void RoundLose()
    {
        sequenceControl.Clear();
        animator.SetTrigger("lose");
        countdown.Terminate();
        audioSource.clip = loseSFX;
        audioSource.Play();
        StartCoroutine(WaitAndFireNextBank());
    }
    
    IEnumerator WaitAndFireNextBank()
    {
        yield return new WaitForSeconds(1f);
        RoundStart();
    }
}