using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testerTurret : AITurret
{
    [SerializeField] GameObject target;
    Rigidbody2D mRigidBody;
    Transform targetTransform;
    Transform mTransform;
    Vector2 targetPosition;
    InputPacket ip;
    float desireAngle;
    float mAngle;
    float dir;
    public float tolerance;
    public bool smoothAim;
    // Start is called before the first frame update
    void Start()
    {
        mRigidBody = GetComponent<Rigidbody2D>();
        mTransform = this.transform;
        targetTransform = target.transform;
        ip = new InputPacket();
    }

    // Update is called once per frame
    void Update()
    {
        mAngle = mRigidBody.rotation;
        targetPosition = new Vector2(targetTransform.position.x - mTransform.position.x, targetTransform.position.y - mTransform.position.y);
        desireAngle = Mathf.Atan2(targetPosition.y, targetPosition.x) * Mathf.Rad2Deg;
        dir = CalcShortestRoute(mAngle, desireAngle);
        if (dir > 0)
            ip.inputTurret = 1;
        else if (dir < 0)
            ip.inputTurret = -1;
        else
            ip.inputTurret = 0;
        Debug.Log(ip.inputTurret);
    }

    float CalcShortestRoute(float from, float to)
    {
        //Debug.Log("f:" + from + "t:" + to);
        if (from < 0)
        {
            from += 360;
        }
        if (to < 0)
        {
            to += 360;
        }
        //Debug.Log(Mathf.DeltaAngle(from, to));
        if (Mathf.Abs(Mathf.DeltaAngle(from,to)) < tolerance)
        {
            return 0;
        }
        float left = (360 - from) + to;
        float right = from - to;

        if (from < to)
        {
            if (to > 0)
            {
                left = to - from;
                right = (360 - to) + from;
            }
            else
            {
                left = (360 - to) + from;
                right = to - from;
            }
        }
        if (left <= right) return 1;
        else if (right <= left) return -1;
        else return 0;
        //return ((left <= right) ? left : (right * -1));
    }

    public override InputPacket GetInputPacket()
    {
        return ip;
    }
}
