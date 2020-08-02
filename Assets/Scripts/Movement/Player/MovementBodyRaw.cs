using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBodyRaw : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rgd;
    AIScript input; 
    public float MoveSpeed;
    public bool playerControl;
    InputPacket ip;
    void Start()
    {
        rgd = GetComponent<Rigidbody2D>();
        ip = new InputPacket();
        input = GetComponent<AIScript>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ip = input.GetInputPacket(ip);
        Move(ip);

    }

    public void Move(InputPacket ip)
    {
        rgd.position = new Vector2(rgd.position.x - ip.inputRotate * MoveSpeed * Time.fixedDeltaTime, rgd.position.y + ip.inputTranslate * MoveSpeed * Time.fixedDeltaTime);
    }
}
