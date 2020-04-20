using System;
using System.Collections.Generic;
using UnityEngine;

public class SceneDirector : MonoBehaviour
{
    public TextAsset script;

    void Awake()
    {
        Parse();
    }

    void Parse()
    {
        var sceneScript = JsonUtility.FromJson<SceneScript>(script.text);
        
    }
}

[Serializable]
public class SceneScript
{
    public List<Instruction> instructions;
}

[Serializable]
public class Instruction
{
    public string type;
    public string content;
}
