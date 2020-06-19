using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderHealthBodyChange : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject player;
    SpriteRenderer playerSprite;
    Image mImage;
    void Start()
    {
        mImage = GetComponent<Image>();
        player = FindObjectOfType<PlayerInputBody>().gameObject;
        playerSprite = player.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        mImage.sprite = playerSprite.sprite;
    }
}
