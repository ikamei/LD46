using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;

public class MengNanValue : MonoBehaviour {
    double value;
    
    double GetValue()
    {
        return value;
    }
    void SetValue(double _value)
    {
        value = _value;
    }
}
