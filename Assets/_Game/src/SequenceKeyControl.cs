using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SequenceKeyControl : MonoBehaviour
{
    public Direction value;
    public Sprite sprUp, sprLeft, sprDown, sprRight;
    public Image image;

    public enum Direction
    {
        Up,
        Left,
        Down,
        Right
    }

    Dictionary<Direction, Sprite> mapping;

    void Awake()
    {
        mapping = new Dictionary<Direction, Sprite>
        {
            [Direction.Up] = sprUp,
            [Direction.Left] = sprLeft,
            [Direction.Down] = sprDown,
            [Direction.Right] = sprRight
        };
    }

    public void Initialize(Direction dir)
    {
        value = dir;
        image.sprite = mapping[dir];
    }

    public static Direction RandomDirection()
    {
        return (Direction) Random.Range(0, 4);
    }
}
