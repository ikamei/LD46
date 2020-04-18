using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;

public class MengNanValue : MonoBehaviour {
    
    // for inspector debug
    //[SerializeField]
    int m_value;
    List<Sprite> m_numbers;
    void Start()
    {
        m_numbers = new List<Sprite>();
        string[] strNumbers = { "number_0", "number_1", "number_2", "number_3", "number_4", "number_5", "number_6", "number_7", "number_8", "number_9" };
        for( int j=0; j<10; ++j )
        {
            Texture2D tex = Resources.Load<Texture2D>(strNumbers[j]);
            m_numbers.Add( Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f) );
        }

        SetValue(100);
    }

    public int GetValue()
    {
        return m_value;
    }

    public void SetValue(int _value)
    {
        m_value = _value;
        updateGUI();
    }

    void updateGUI()
    {
        int numberCount;
        if( m_value < 10 )
            numberCount = 1;
        else if( m_value < 100 )
            numberCount = 2;
        else if( m_value < 1000 )
            numberCount = 3;
        else
            return;
        List<Sprite> numbers = new List<Sprite>();
        if( numberCount == 3 )
        {
            int value = m_value / 100;
            numbers.Add( UnityEngine.Object.Instantiate(m_numbers[value]) );

            value = m_value / 10;
            int idx = value % 10;
            numbers.Add( UnityEngine.Object.Instantiate(m_numbers[idx]) );

            value = (m_value / 10) * 10;
            idx = m_value - value;
            numbers.Add( UnityEngine.Object.Instantiate(m_numbers[idx]) );
        }
        if( numberCount == 2 )
        {
            int idx = m_value / 10;
            numbers.Add( UnityEngine.Object.Instantiate(m_numbers[idx]) );

            idx = m_value % 10;
            numbers.Add( UnityEngine.Object.Instantiate(m_numbers[idx]) );
        }
        if( numberCount == 1 )
        {
            int idx = m_value;
            numbers.Add( UnityEngine.Object.Instantiate(m_numbers[idx]) );
        }

        foreach (Transform child in gameObject.transform)
        {
            Destroy(child.gameObject);
        }


        GameObject middle = new GameObject();
        middle.transform.localPosition = new Vector3(0,0,0);
        // middle.transform.position = new Vector3(0,0,0);
        middle.transform.SetParent(gameObject.transform,false);
        // Debug.Log( "name = " + middle.name+ "middle.transform.localPosition0 = " + middle.transform.localPosition );
        List<GameObject> goes = new List<GameObject>();
        for( int j=0; j<numbers.Count; ++j )
        {
            GameObject go = new GameObject();
            go.transform.localScale = new Vector3(100,100,100);
            goes.Add( go );
            SpriteRenderer renderer = go.AddComponent<SpriteRenderer>();
            // renderer.enabled = 1==is_enable;
            renderer.sortingOrder = 1;
            renderer.sprite = numbers[j];
            go.transform.SetParent(middle.transform,false);
        }
        // Debug.Log( "numbers[0].textureRect.width = " + numbers[0].textureRect.width );
        // Debug.Log( "numbers[0].rect.width = " + numbers[0].rect.width );

        if( numberCount == 3 )
        {
            goes[0].transform.Translate( new Vector3(-1f*(numbers[0].rect.width + numbers[1].rect.width / 2), 0, 0) );
            goes[2].transform.Translate( new Vector3(1f*(numbers[2].rect.width + numbers[1].rect.width / 2), 0, 0) );
        }
        else if( numberCount == 2 )
        {
            goes[0].transform.Translate( new Vector3(-1f*(numbers[0].rect.width / 2), 0, 0) );
            goes[1].transform.Translate( new Vector3(1f*(numbers[1].rect.width / 2), 0, 0) );
        }

        // Debug.Log( "middle.transform.localPosition = " + middle.transform.localPosition );
        // Debug.Log( "middle.transform.position = " + middle.transform.position );
    }
}
