using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIScript : MonoBehaviour
{
    InputPacket ip;
    public virtual InputPacket GetInputPacket()
    {
        return ip;
    }

    public virtual InputPacket GetInputPacket(InputPacket nip)
    {
        return nip;
    }
}
