using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputTurret : MonoBehaviour
{
    RotationTurret mTurret;
    InputPacket ip;

    private void Start()
    {
        mTurret = GetComponent<RotationTurret>();
        ip = new InputPacket();
    }
    private void FixedUpdate()
    {
        ip.inputTurret = Input.GetAxis("Rotation Turret");
        //Debug.Log("Send info yeas:" + ip.inputTurret);
        mTurret.SendInputPacket(ip);
    }
}
