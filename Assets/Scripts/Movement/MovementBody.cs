using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBody : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rgd;
    PlayerInputBody playerInput;
    public float maxMoveSpeed;
    public float accelMoveSpeed;
    public float maxRotSpeed;
    public float accelRotSpeed;
    public float curMoveSpeed;
    public float curRotSpeed;
    public bool playerControl;
    InputPacket ip;
    float xvel;
    float yvel;
    float angle;
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
        angle = rgd.rotation;
        xvel = (Mathf.Rad2Deg * Time.fixedDeltaTime * Mathf.Cos(angle * Mathf.Deg2Rad)) * curMoveSpeed;
        yvel = (Mathf.Rad2Deg * Time.fixedDeltaTime * Mathf.Sin(angle * Mathf.Deg2Rad)) * curMoveSpeed;
        rgd.velocity = new Vector2(xvel, yvel);
        rgd.angularVelocity = curRotSpeed;

        if (playerControl)
            ip = playerInput.getInputPacket(ip);
        changeTranslate(ip);
        changeRotate(ip);
        killTranslate(ip);
        killRotate(ip);

    }

    public void changeTranslate(InputPacket ip)
    {
        if (curMoveSpeed < maxMoveSpeed && ip.inputTranslate >= .1)
        {
            if (curMoveSpeed + (accelMoveSpeed * Time.fixedDeltaTime) >= maxMoveSpeed)
            {
                curMoveSpeed = maxMoveSpeed;
                return;
            }
            curMoveSpeed += accelMoveSpeed * Time.fixedDeltaTime;
            return;
        }

        if (curMoveSpeed > -maxMoveSpeed && ip.inputTranslate <= -.1)
        {
            if (curMoveSpeed - (accelMoveSpeed * Time.fixedDeltaTime) <= -maxMoveSpeed)
            {
                curMoveSpeed = -maxMoveSpeed;
                return;
            }
            curMoveSpeed -= accelMoveSpeed * Time.fixedDeltaTime;
            return;
        }
    }

    public void changeRotate (InputPacket ip)
    {
        if (curRotSpeed < maxRotSpeed && ip.inputRotate >= .1)
        {
            if (curRotSpeed + (accelRotSpeed * Time.fixedDeltaTime) >= maxRotSpeed)
            {
                curRotSpeed = maxRotSpeed;
                return;
            }
            curRotSpeed += accelRotSpeed * Time.fixedDeltaTime;
            return;
        }

        if (curRotSpeed > -maxRotSpeed && ip.inputRotate <= -.1)
        {
            if (curRotSpeed - (accelRotSpeed * Time.fixedDeltaTime) <= -maxRotSpeed)
            {
                curRotSpeed = -maxRotSpeed;
                return;
            }
            curRotSpeed -= accelRotSpeed * Time.fixedDeltaTime;
            return;
        }
    }
    
    public void killTranslate (InputPacket ip)
    {
        if (ip.cancelTranslate)
        {
            if (curMoveSpeed > accelMoveSpeed * 3f * Time.fixedDeltaTime)
            {
                curMoveSpeed -= accelMoveSpeed * 3f * Time.fixedDeltaTime;
            }
            else if (curMoveSpeed < -accelMoveSpeed * 3f * Time.fixedDeltaTime)
            {
                curMoveSpeed += accelMoveSpeed * 3f * Time.fixedDeltaTime;
            }
            else
            {
                curMoveSpeed = 0f;
            }
        }
    }

    public void killRotate (InputPacket ip)
    {
        if (ip.cancelRotate)
        {
            if (curRotSpeed > accelRotSpeed * 3f * Time.fixedDeltaTime)
            { //For Rotation
                curRotSpeed -= accelRotSpeed * 3f * Time.fixedDeltaTime;
            }
            else if (curRotSpeed < -accelRotSpeed * 3f * Time.fixedDeltaTime)
            {
                curRotSpeed += accelRotSpeed * 3f * Time.fixedDeltaTime;
            }
            else
            {
                curRotSpeed = 0f;
            }
        }
    }
}
