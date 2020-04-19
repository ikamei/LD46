using UnityEngine;
using System.Collections.Generic;
using System;

[Serializable]
public class StopWatch : MonoBehaviour
{
    System.DateTime m_start_tick;
    bool m_stopwatch;
    const int m_precision = 100;
    int m_count = m_precision;
    int m_time_elapse = 100;
    List<GameObject> m_gears;
    MasterAI m_master_ai;
    
    void Start()
    {
        m_gears = new List<GameObject>();
        StopTheWatch();

        string[] names = { "stop_watch_disk", "stop_watch_needle" };

        GameObject middle = new GameObject();
        middle.transform.SetParent(gameObject.transform,false);
        middle.transform.localScale = new Vector3(0.45f,0.45f,0.45f);
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
        //questionCtrl.AskQuestion();
        GameObject master_go = GameObject.Find("Master");
        m_master_ai = master_go.GetComponent<MasterAI>();
        //StartTheWatch();
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
        if(false)
        {
            GameObject mengnan_value = GameObject.Find("MengNanValue");
            MengNanValue mengnan_component = mengnan_value.GetComponent<MengNanValue>();
            int value = mengnan_component.GetValue();
            mengnan_component.SetValue(value-10);
        }

        {
            // Debug.Log("it should ask next question");
            // GameObject go = GameObject.Find("Question Panel");
            // 使用 GameObject.Find 有可能找到的是 prefab，而非场景中的对象
            m_master_ai.set_state( MyConst.ACTION_STATE_ANSWER_TIMEOUT );
            // QuestionController component = questionCtrl;
            // component.AskQuestion();
            //StartTheWatch();
        }
    }
    
    void Update()
    {
        if( true == m_stopwatch )
        {
            return;
        }
        if( m_count <= 0 )
        {
            StopTheWatch();
            TimeOut();
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
