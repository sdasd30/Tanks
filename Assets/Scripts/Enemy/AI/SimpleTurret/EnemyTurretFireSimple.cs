using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurretFireSimple : MonoBehaviour
{
    // Start is called before the first frame update
    InputPacket ip;
    FireProjectile fp;
    void Start()
    {
        ip = new InputPacket();
        fp = GetComponent<FireProjectile>();
        ip.inputAction1 = true;
    }

    // Update is called once per frame
    void Update()
    {
        fp.SendInputPacket(ip);
    }
}
