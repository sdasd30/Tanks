using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurretAimSimple : AITurret
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
        target = FindObjectOfType<PlayerInputBody>().gameObject;
        mRigidBody = GetComponent<Rigidbody2D>();
        mTransform = this.transform;
        targetTransform = target.transform;
        ip = new InputPacket();
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
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
        }
        else
            ip.inputTurret = 0;
    }

    float CalcShortestRoute(float from, float to)
    {
        //Debug.Log("f:" + from + "t:" + to);
        from %= 360;
        to %= 360;
        if (from < 0)
        {
            from += 360;
        }
        if (to < 0)
        {
            to += 360;
        }
        //Debug.Log(Mathf.DeltaAngle(from, to));
        if (Mathf.Abs(Mathf.DeltaAngle(from, to)) < tolerance)
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
    /*
    [SerializeField] GameObject target;
    Rigidbody2D mRigidBody;
    Transform targetTransform;
    Transform mTransform;
    Vector2 targetPosition;
    Vector2 mPosition;
    RotationTurretRaw mTurret;
    InputPacket ip;
    float desireAngle;
    float mAngle;
    public float tolerance;
    public bool smoothAim;

    // Start is called before the first frame update
    void Start()
    {
        target = FindObjectOfType<PlayerInputBody>().gameObject;
        targetTransform = target.GetComponent<Transform>();
        mTurret = GetComponent<RotationTurretRaw>();
        mTransform = GetComponent<Transform>();
        mRigidBody = GetComponent<Rigidbody2D>();
        ip = new InputPacket();
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            targetPosition = new Vector2(targetTransform.position.x - mTransform.position.x, targetTransform.position.y - mTransform.position.y);
            desireAngle = Mathf.Atan2(targetPosition.y, targetPosition.x) * Mathf.Rad2Deg;
            mAngle = mRigidBody.rotation;
        }
    }

    InputPacket CalculateIP(float dAn, float mAn)
    {
        float modVal = 1;
        float coolBool = 1;
        if (dAn < 0)
        {
            dAn += 360;
        }
        if (mAn < 0)
        {
            mAn += 360;
        }
        float alternatemAN = (mAn < 180) ? mAn + 360 : mAn - 360;
        //Debug.Log("Target: " + dAn + " mine: " + mAn);
        if (Mathf.Abs(alternatemAN - dAn) < Mathf.Abs(mAn - dAn))
        {
            coolBool = -1;
        }
        if (smoothAim)
            modVal = Mathf.Abs(dAn - mAn) / 90f;

        if (dAn-mAn >= tolerance)
        {
            //Debug.Log("ip1");
            ip.inputTurret = 1 * modVal * coolBool;

        }
        else if (mAn - dAn >= tolerance)
        {
            //Debug.Log("ip-1");
            ip.inputTurret = -1 * modVal * coolBool;
        }
        else
            ip.inputTurret = 0;
        return ip;
    }

    public override InputPacket GetInputPacket()
    {
        return ip;
    }*/
}
