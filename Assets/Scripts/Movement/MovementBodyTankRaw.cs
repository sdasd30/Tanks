using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBodyTankRaw : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rgd;
    AIScript input;
    public float maxMoveSpeed;
    public float maxRotSpeed;
    float moveSpeed;
    float rotSpeed;
    InputPacket ip;
    float xvel;
    float yvel;
    float angle;
    void Start()
    {
        rgd = GetComponent<Rigidbody2D>();
        ip = new InputPacket();
        input = GetComponent<AIScript>();
        Debug.Log(input);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        angle = rgd.rotation;
        xvel = (Mathf.Rad2Deg * Time.fixedDeltaTime * Mathf.Cos(angle * Mathf.Deg2Rad)) * moveSpeed;
        yvel = (Mathf.Rad2Deg * Time.fixedDeltaTime * Mathf.Sin(angle * Mathf.Deg2Rad)) * moveSpeed;
        rgd.velocity = new Vector2(xvel, yvel);
        rgd.angularVelocity = rotSpeed;
        ip = input.GetInputPacket(ip);
        ChangeTranslate(ip);
        ChangeRotate(ip);
    }

    public void ChangeTranslate(InputPacket ip)
    {
       // Debug.Log("translate " + ip.inputTranslate);
        if (ip.inputTranslate > .1f)
        {
            moveSpeed = maxMoveSpeed;
        }
        else if (ip.inputTranslate < -.1f)
        {
            moveSpeed = -maxMoveSpeed;
        }
        else
            moveSpeed = 0f;
    }

    public void ChangeRotate(InputPacket ip)
    {
     //   Debug.Log("rotate " + ip.inputRotate);
        if (ip.inputRotate > .1f)
        {
            rotSpeed = maxRotSpeed;
        }
        else if (ip.inputRotate < -.1f)
        {
            rotSpeed = -maxRotSpeed;
        }
        else
            rotSpeed = 0f;
    }
}
