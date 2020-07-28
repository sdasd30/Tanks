using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderHealth : MonoBehaviour
{
    GameObject player;
    Attackable playerHealth;
    Slider mSlider;
    public GameObject fillObj;
    Image fill;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerInputBody>().gameObject;
        playerHealth = player.GetComponent<Attackable>();
        mSlider = GetComponent<Slider>();
        fill = fillObj.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(1 - (playerHealth.getMax() / playerHealth.getHealth()));
        if (player == null)
        {
            mSlider.value = 0;
            return;
        }
        mSlider.value = (playerHealth.getHealth()/ playerHealth.getMax());
        fill.color = getColor(playerHealth.getHealth(), playerHealth.getMax());

    }

    Color32 getColor(float hp, float max)
    {
        Debug.Log(hp +"/" + max);
        Color32 color = new Color32(0, 0, 255, 255);
        if (hp >= max / 2)
        {
            Debug.Log("groen");
            float math = (255) * (1 - ((hp - (max / 2)) / (max / 2)));
            return new Color32((byte)math, 255, 0, 200);
        }

        else
        {
            Debug.Log("roed");
            float math = (255) * (hp / (max / 2));
            return new Color32(255, (byte)math, 0, 200);
        }

    }
}