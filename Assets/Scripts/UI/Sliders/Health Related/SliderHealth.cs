using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderHealth : MonoBehaviour
{
    GameObject player;
    Attackable playerHealth;
    Slider mSlider;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerInputBody>().gameObject;
        playerHealth = player.GetComponent<Attackable>();
        mSlider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(1 - (playerHealth.getMax() / playerHealth.getHealth()));
        mSlider.value = ( 1 - (playerHealth.getHealth()/ playerHealth.getMax()));
    }
}