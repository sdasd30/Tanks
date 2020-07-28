using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputTurretAction1 : MonoBehaviour
{
    FireProjectile mTurret;
    InputPacket ip;

    private void Start()
    {
        mTurret = GetComponent<FireProjectile>();
        ip = new InputPacket();
    }
    private void Update()
    {
        ip.inputAction1 = Input.GetButton("Action1");
        ip.inputReload = Input.GetButton("Reload");
        mTurret.SendInputPacket(ip);
    }

}

