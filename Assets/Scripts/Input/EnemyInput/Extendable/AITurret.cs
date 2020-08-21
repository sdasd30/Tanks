using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITurret : MonoBehaviour
{
    InputPacket ip;
    public virtual InputPacket GetInputPacket()
    {
        return ip;
    }

    public InputPacket GetInputPacket(InputPacket nIp)
    {
        return nIp;
    }
}
