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

    void updateGUI()
    {
        // if( slot_index<0 || slot_index>=item_images.Length )
        //     return;
        
        // Texture2D tex = (Texture2D)Resources.Load( item_image, typeof(Texture2D) );
        // Sprite mySprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);

        // items[slot_index].active = true;
        // item_images[slot_index].GetComponent<Image>().sprite = mySprite;
        // item_names[slot_index].GetComponent<Text>().text     = item_name;
        // item_prices[slot_index].GetComponent<Text>().text    = "" + price;
        // item_data[slot_index]                                = _item_data;
    }




}
