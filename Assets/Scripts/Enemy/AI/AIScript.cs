using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIScript : MonoBehaviour
{

    public virtual InputPacket GetInputPacket(InputPacket ip)
    {
        return ip;
    }
}
