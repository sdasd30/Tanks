using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBodyRaw : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rgd;
    PlayerInputBody playerInput;
    public float MoveSpeed;
    public bool playerControl;
    InputPacket ip;
    void Start()
    {
        rgd = GetComponent<Rigidbody2D>();
        ip = new InputPacket();
        if (playerControl)
            playerInput = GetComponent<PlayerInputBody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (playerControl)
            ip = playerInput.getInputPacket(ip);
        Move(ip);

    }

    public void Move(InputPacket ip)
    {
        rgd.position = new Vector2(rgd.position.x - ip.inputRotate * MoveSpeed * Time.fixedDeltaTime, rgd.position.y + ip.inputTranslate * MoveSpeed * Time.fixedDeltaTime);
    }
}
