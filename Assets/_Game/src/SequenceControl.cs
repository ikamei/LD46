using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static SequenceKeyControl;

public class SequenceControl : MonoBehaviour
{
    public int length;
    public SequenceKeyControl sequenceKeyPrefab;
    public AudioClip progressSFX;
    public AudioSource audioSource;
    
    public UnityEvent onSuccess, onFailure;
    
    Queue<SequenceKeyControl> keys;

    void Awake()
    {
        keys = new Queue<SequenceKeyControl>();
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
            Progress();
        }
        else
        {
            onFailure?.Invoke();
        }
    }

    void Progress()
    {
        var key = keys.Dequeue();
        Destroy(key.gameObject);
        if (keys.Count == 0)
        {
            onSuccess?.Invoke();
        }
        else
        {
            audioSource.clip = progressSFX;
            audioSource.Play();
        }
    }

    Direction Peek()
    {
        var key = keys.Peek();
        return key.value;
    }

    public void Clear()
    {
        foreach (var key in keys)
        {
            Destroy(key.gameObject);
        }
        keys = new Queue<SequenceKeyControl>();
    }
}