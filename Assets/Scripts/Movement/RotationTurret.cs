using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationTurret : MonoBehaviour
{
    Transform m_body;
    PlayerInputTurret playerInput;
    public float maxRotSpeed;
    public float accelRotSpeed;
    public float curRotSpeed;
    public float slowRotSpeed = .05f;
    InputPacket ip;
    float desireRot;
    // Start is called before the first frame update
    void Start()
    {
        m_body = GetComponent<Transform>();
        ip = new InputPacket();
        playerInput = GetComponent<PlayerInputTurret>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ip = playerInput.getInputPacket(ip);
        desireRot += curRotSpeed * Time.fixedDeltaTime;
        m_body.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, desireRot));
        updateSpeed(ip);
    }

    public void updateSpeed(InputPacket ip)
    {
        if (ip.inputTurret > .1f)
        {
            if (curRotSpeed < maxRotSpeed)
            {
                curRotSpeed += accelRotSpeed * Time.fixedDeltaTime;
            }
        }
        else if (ip.inputTurret < -.1f)
        {
            if (curRotSpeed > -maxRotSpeed)
            {
                curRotSpeed -= accelRotSpeed * Time.fixedDeltaTime;
            }
        }
        else
            slowRotation(ip);
    }

    public void slowRotation(InputPacket ip)
    {
        if (curRotSpeed > 0)
        {
            if (curRotSpeed < accelRotSpeed * slowRotSpeed * Time.fixedDeltaTime)
            {
                curRotSpeed = 0f;
                return;
            }
            curRotSpeed -= accelRotSpeed * slowRotSpeed * Time.fixedDeltaTime;
            return;
        }
        else if (curRotSpeed < 0)
        {
            if (curRotSpeed > accelRotSpeed * slowRotSpeed * Time.fixedDeltaTime)
            {
                curRotSpeed = 0f;
            }
            curRotSpeed += accelRotSpeed * slowRotSpeed * Time.fixedDeltaTime;
            return;
        }
            
    }
}
