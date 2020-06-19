using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputTurret : MonoBehaviour
{
    public InputPacket getInputPacket(InputPacket ip)
    {
        ip.inputTurret = Input.GetAxis("Rotation Turret");

        return ip;
    }
}
