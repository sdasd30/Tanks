using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextHealth : MonoBehaviour
{
    // Start is called before the first frame update
    TextMeshProUGUI text;
    Attackable player;
    public bool colorShift;
    int maxHP;
    int displayHP;
    void Start()
    {
       
        player = FindObjectOfType<PlayerInputBody>().GetComponent<Attackable>();
        
    }

    // Update is called once per frame
    void Update()
    {
        text = GetComponent<TextMeshProUGUI>();
        if (player == null)
        {
            text.SetText("ERROR");
            text.color = new Color32 (255,0,0,255);
            return;
        }
        if (player.getHealth() < 0)
        {
            
        }
        else
        {
            text.SetText("{0}/{1}", player.getHealth(), player.getMax());
        }
        if (colorShift)
        {
            text.color = getColor(player.getHealth(), player.getMax());
        }
    }

    Color32 getColor(float hp, float max)
    {
        Color32 color = new Color32 (0, 255, 0, 255);
        if (hp >= max / 2)
        {
            float math = (255) * (1 - ((hp - (max / 2)) / (max / 2)));
            return new Color32((byte)math, 255, 0, 255);
        }

        else
        {
            float math = (255) * (hp / (max / 2));
            return new Color32(255, (byte)math, 0, 255);
        }
        
    }
}
