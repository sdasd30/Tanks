using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class AITank : AIScript
{
    public float stopDistance; //When will the AI stop moving towards the target?
    Path path;
    public Transform target; //Defaults to the Player
    public float distBuffer = .1f; //How far can this get from the node while being okay?
    public float angleBuffer = 10f; //How far can it be the wrong angle before it says, OK!
    Rigidbody2D mRigidBody;
    int currentWaypoint;

    private void Start()
    {
        mRigidBody = GetComponent<Rigidbody2D>();
        if (target == null)
        {
            target = FindObjectOfType<PlayerInputBody>().transform;
        }
        path = RequestPath();

        //StartCoroutine("PathCheck");
    }

    private void Update()
    {
        transform.LookAt(path.vectorPath[currentWaypoint]);
        Debug.Log("L00king at " + path.vectorPath[currentWaypoint]);
        if (Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]) < distBuffer)
        {
            Debug.Log("I'm close to waypoint " + currentWaypoint);
            currentWaypoint++;
        }
        
    }

    Path RequestPath()
    {
        //Debug.LogWarning("New Path");
        currentWaypoint = 0;
        return GetComponent<Seeker>().StartPath(transform.position, target.position);
    }

    float CalcAngle(Vector3 targetPosition)
    {
        float num;
        float dAn = Mathf.Atan2(targetPosition.y, targetPosition.x) * Mathf.Rad2Deg;
        float mAn = mRigidBody.rotation;
        float coolBool = 1;
        float modVal = -1;
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

        if (dAn - mAn >= angleBuffer)
        {
            //Debug.Log("ip1");
            num = 1 * modVal * coolBool;

        }
        else if (mAn - dAn >= angleBuffer)
        {
            //Debug.Log("ip-1");
            num = -1 * modVal * coolBool;
        }
        else
            num = 0;
        return num;
    }

    float OkAngle()
    {
        

        return 0;
    }

    /*
    public override InputPacket GetInputPacket(InputPacket ip)
    {

        ip.reset();
        return ip;
    }
    */
    public override InputPacket GetInputPacket(InputPacket ip)
    {

        //ip.reset();
        Vector3 mPos = this.transform.position;
        Vector3 desirePos = path.vectorPath[currentWaypoint];
        float dist = Vector3.Distance(mPos, desirePos);
        ip.inputTranslate = 1;
        ip.inputRotate = CalcAngle(desirePos);
        return ip;
    }

    IEnumerator PathCheck()
    {
        yield return new WaitForSeconds(Random.Range(0f, .5f));
        for(; ; )
        {
            path = RequestPath();
            yield return new WaitForSeconds(.3f);
        }
    }

    /*private void DebugPathWaypoints(Path p)
    {
        for (int i = 0; i < p.vectorPath; i++)
        {

        }
    }*/
}
