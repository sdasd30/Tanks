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
    void Start()
    {
        rgd = GetComponent<Rigidbody2D>();
        ip = new InputPacket();
        input = GetComponent<AIScript>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ip = input.GetInputPacket(ip);
        ChangeTranslate(ip);
        ChangeRotate(ip);
        xvel = (Mathf.Rad2Deg * Time.fixedDeltaTime * Mathf.Cos(rgd.rotation * Mathf.Deg2Rad)) * moveSpeed;
        yvel = (Mathf.Rad2Deg * Time.fixedDeltaTime * Mathf.Sin(rgd.rotation * Mathf.Deg2Rad)) * moveSpeed;
        rgd.velocity = new Vector2(xvel, yvel);
        rgd.angularVelocity = rotSpeed;
    }

    public void ChangeTranslate(InputPacket ip)
    {
        //Debug.Log("translate " + ip.inputTranslate);
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
