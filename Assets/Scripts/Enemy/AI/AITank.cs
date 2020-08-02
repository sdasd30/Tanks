using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITank : AIScript
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override InputPacket GetInputPacket(InputPacket ip)
    {
        ip.reset();
        return ip;
    }
}
