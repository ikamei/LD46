using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;

public class StopWatch : MonoBehaviour {
    System.DateTime m_start_tick;
    bool m_stopwatch;
    const int m_precision = 100;
    int m_count = m_precision;
    int m_time_elapse = 100;
    List<GameObject> m_gears;
    void Start()
    {
        m_gears = new List<GameObject>();
        StopTheWatch();

        string[] names = { "stop_watch_disk", "stop_watch_needle" };

        GameObject middle = new GameObject();
        middle.transform.SetParent(gameObject.transform,false);
        for( int j=0; j<2; ++j )
        {
            GameObject go = new GameObject();
            SpriteRenderer renderer = go.AddComponent<SpriteRenderer>();
            renderer.sortingOrder = 2+j;
            Texture2D tex = Resources.Load<Texture2D>(names[j]);
            renderer.sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 1.0f);
            go.transform.SetParent(middle.transform,false);
            m_gears.Add( go );
        }
        StartTheWatch();
    }

    public void StartTheWatch()
    {
        m_start_tick = System.DateTime.Now;
        m_stopwatch = false;
        m_count = m_precision;
    }

    public void StopTheWatch()
    {
        m_stopwatch = true;
    }

    public void TimeOut()
    {
    }
    
    void Update()
    {
        if( true == m_stopwatch )
        {
            return;
        }
        if( m_count <= 0 )
        {
            TimeOut();
            StopTheWatch();
        }
        System.TimeSpan span = System.DateTime.Now - m_start_tick;
        if( span.TotalMilliseconds > m_time_elapse )
        {
            m_start_tick = System.DateTime.Now;
            m_count--;
            updateGUI();
        }
    }


    void updateGUI()
    {
        double time_total = m_precision * m_time_elapse;
        double time_elapse = (m_count-m_precision) * m_time_elapse;
        double angle = 360 * time_elapse / time_total;
        m_gears[1].transform.localRotation = Quaternion.Euler(0, 0, (float)angle);
    }
}
