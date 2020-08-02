using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputBody : AIScript
{
    public override InputPacket GetInputPacket(InputPacket ip)
    {
        ip.reset();
        ip.inputTranslate = Input.GetAxis("Translation");
        ip.inputRotate = Input.GetAxis("Rotation Body");
        

        if (Input.GetKey("x"))
        {
            ip.cancelRotate = true;
            ip.cancelTranslate = true;
        }
        if (Input.GetKey("z"))
        {
            ip.cancelTranslate = true;
        }
        if (Input.GetKey("c"))
        {
            ip.cancelRotate = true;
        }

        return ip;
    }

}
