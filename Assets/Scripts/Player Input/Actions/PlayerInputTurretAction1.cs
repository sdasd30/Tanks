using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputTurretAction1 : MonoBehaviour
{
    public InputPacket GetInputPacket(InputPacket ip)
    {
        ip.inputAction1 = Input.GetButton("Action1");
        ip.inputReload = Input.GetButton("Reload");
        return ip;
    }
}
