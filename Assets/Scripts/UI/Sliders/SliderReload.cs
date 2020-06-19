using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderReload : MonoBehaviour
{
    GameObject player;
    FireProjectile playerProjectile;
    Slider mSlider;
    float modVal;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerInputTurret>().gameObject;
        playerProjectile = player.GetComponent<FireProjectile>();
        mSlider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        modVal = 1 / playerProjectile.getCooldownMax();
        mSlider.value = (modVal * playerProjectile.getCurrentCooldown());
    }
}