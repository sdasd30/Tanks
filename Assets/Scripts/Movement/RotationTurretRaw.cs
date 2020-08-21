using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationTurretRaw : MonoBehaviour
{
    
    Transform m_body;
    AITurret inputSource;
    GunStats gs;
    float rotSpeed = 45;
    [SerializeField] float curRotSpeed;
    float curRot;
    InputPacket ip;
    float desireRot;
    // Start is called before the first frame update
    void Start()
    {
        m_body = GetComponent<Transform>();
        gs = GetComponent<GunStats>();
        inputSource = GetComponent<AITurret>();
        if (gs != null)
            rotSpeed = gs.maxRotSpeed;
        ip = new InputPacket();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ip = inputSource.GetInputPacket();
        desireRot += curRot * Time.fixedDeltaTime;
        m_body.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, desireRot));
        UpdateSpeed(ip);
    }

    public void UpdateSpeed(InputPacket ip)
    {
        if (ip.inputTurret > .1f)
        {
            curRot = rotSpeed * Mathf.Abs(ip.inputTurret);
        }
        else if (ip.inputTurret < -.1f)
        {
            curRot = -rotSpeed * Mathf.Abs(ip.inputTurret);
        }
        else
            curRot = 0;
    }
}
