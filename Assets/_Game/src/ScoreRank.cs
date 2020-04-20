using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ScoreRank : MonoBehaviour
{
    MasterAI m_master_ai;
    void Awake()
    {
        GameObject master_go = GameObject.Find("Master");
        m_master_ai = master_go.GetComponent<MasterAI>();
    }
    public void updateGUI()
    {
        // get rank, 0a, 1b, 2c, 3man
        int rank = 0;
        int score = (int)m_master_ai.score();
        bool isMacho = m_master_ai.isMacho();
        if( true == isMacho )
        {
            rank = 3;            
        }
        else
        {
            if( 100 == score )
            {
                rank = 0;
            }
            else if(score>0 && score<100)
            {
                rank = 1;
            }
            else if(0==score)
            {
                rank = 2;
            }
            else
            {
                return;
            }
        }

        string[] temp_name = { "gameover_a", "gameover_b", "gameover_c", "gameover_man" };
        GameObject go;
        GameObject active_go = GameObject.Find( temp_name[rank] );
        for( int j=0; j<4; ++j )
        {
            go = GameObject.Find( temp_name[j] );
            go.active = false;
        }

        active_go.active = true;
    }
}