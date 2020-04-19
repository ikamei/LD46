using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Resource Mapper")]
public class ResourceMapper : ScriptableObject
{
    public Dictionary<int, Sprite> mapping;
}
