using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationTurret : MonoBehaviour
{
    Transform m_body;
    GunStats gs;
    float maxRotSpeed = 180;
    float accelRotSpeed = 360;
    [SerializeField] float curRotSpeed;
    float slowRotSpeed = 2f;
    InputPacket ip;
    float desireRot;
    // Start is called before the first frame update
    void Start()
    {
        m_body = GetComponent<Transform>();
        gs = GetComponent<GunStats>();
        maxRotSpeed = gs.maxRotSpeed;
        accelRotSpeed = gs.accelRotSpeed;
        slowRotSpeed = gs.dampenRotSpeed;
        ip = new InputPacket();
        //if (playerControl)
            //playerInput = GetComponent<PlayerInputTurret>();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //ip = playerInput.getInputPacket(ip);
        desireRot += curRotSpeed * Time.fixedDeltaTime;
        m_body.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, desireRot));
        UpdateSpeed(ip);

    }

    public void UpdateSpeed(InputPacket ip)
    {
        if (ip.inputTurret > .1f)
        {
            if (curRotSpeed < maxRotSpeed)
            {
                curRotSpeed += accelRotSpeed * Time.fixedDeltaTime * Mathf.Abs(ip.inputTurret);
            }
        }
        else if (ip.inputTurret < -.1f)
        {
            if (curRotSpeed > -maxRotSpeed)
            {
                curRotSpeed -= accelRotSpeed * Time.fixedDeltaTime * Mathf.Abs(ip.inputTurret);
            }
        }
        else
            SlowRotation(ip);
    }

    public void SlowRotation(InputPacket ip)
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

    public void SendInputPacket(InputPacket nIP)
    {
        ip = nIP;
    }
}
