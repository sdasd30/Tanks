using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderThrottle : MonoBehaviour
{
    GameObject player;
    MovementBody playerMove;
    Slider mSlider;
    float modVal;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerInputBody>().gameObject;
        playerMove = player.GetComponent<MovementBody>();
        mSlider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        modVal = 1 / playerMove.maxMoveSpeed;
        mSlider.value = (modVal * playerMove.curMoveSpeed);
    }
}
