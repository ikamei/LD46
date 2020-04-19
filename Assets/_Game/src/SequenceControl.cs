using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SequenceKeyControl;

public class SequenceControl : MonoBehaviour
{
    public int length;
    public Countdown countdown;
    public SequenceKeyControl sequenceKeyPrefab;
    public AudioClip successSFX;
    public AudioClip successAllSFX;
    public AudioClip failureSFX;
    public AudioSource audioSource;

    Queue<SequenceKeyControl> keys;

    void Awake()
    {
        keys = new Queue<SequenceKeyControl>();
    }

    void OnGUI()
    {
        if (GUILayout.Button("Create Sequence"))
        {
            CreateSequence();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            PlayerInput(Direction.Up);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            PlayerInput(Direction.Left);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            PlayerInput(Direction.Down);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            PlayerInput(Direction.Right);
        }
    }

    public void CreateSequence()
    {
        Clear();
        countdown.StartCountdown();
        for (var i = 0; i < length; ++i)
        {
            var key = Instantiate(sequenceKeyPrefab, transform);
            key.Initialize(RandomDirection());
            keys.Enqueue(key);
        }
    }

    void PlayerInput(Direction dir)
    {
        if (keys.Count == 0) return;
        if (dir == Peek())
        {
            SuccessOne();
        }
        else
        {
            Fail();
        }
    }

    public void Fail()
    {
        Clear();
        countdown.Terminate();
        audioSource.clip = failureSFX;
        audioSource.Play();
        StartCoroutine(WaitAndFireNextBank());
    }

    void SuccessOne()
    {
        var key = keys.Dequeue();
        Destroy(key.gameObject);
        if (keys.Count == 0)
        {
            SuccessAll();
        }
        else
        {
            audioSource.clip = successSFX;
            audioSource.Play();
        }
    }

    IEnumerator WaitAndFireNextBank()
    {
        yield return new WaitForSeconds(1f);
        CreateSequence();
    }

    void SuccessAll()
    {
        countdown.Terminate();
        audioSource.clip = successAllSFX;
        audioSource.Play();
        StartCoroutine(WaitAndFireNextBank());
    }

    Direction Peek()
    {
        var key = keys.Peek();
        return key.value;
    }

    void Clear()
    {
        foreach (var key in keys)
        {
            Destroy(key.gameObject);
        }
        keys = new Queue<SequenceKeyControl>();
    }
}