using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class AITank : AIScript
{
    public float stopDistance; //When will the AI stop moving towards the target?
    Path path;
    public Transform target; //Defaults to the Player
    public float distBuffer = .01f; //How far can this get from the node while being okay?
    public float angleBuffer = 10f; //How far can it be the wrong angle before it says, OK!
    public float repathRate = .3f; //How much time should pass between path finds?
    Rigidbody2D mRigidBody;
    int currentWaypoint = 2;

    Transform DEBUGOBJECT;

    private void Start()
    {
        DEBUGOBJECT = FindObjectOfType<GunStats>().transform;
        mRigidBody = GetComponent<Rigidbody2D>();
        if (target == null)
        {
            target = FindObjectOfType<PlayerInputBody>().transform;
        }
        path = RequestPath();

        StartCoroutine("PathCheck");
    }

    private void Update()
    {
        DEBUGOBJECT.position = path.vectorPath[currentWaypoint];
        //transform.LookAt(path.vectorPath[currentWaypoint]);
        //Debug.Log("Waypoint #" + currentWaypoint + ": " + path.vectorPath[currentWaypoint]);
        SetAngle();
        if (Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]) < distBuffer)
        {
            Debug.Log("I'm close to waypoint " + currentWaypoint);
            IncrementCurWaypoint();
        }
        if (Input.GetKeyDown("j")) //DEBUG
        {
            IncrementCurWaypoint();
            TryMove();
        }
        if (Input.GetKeyDown("l")) //DEBUG
        {
            path = RequestPath();
        }
    }

    public void IncrementCurWaypoint() //DEBUG
    {
        //Debug.Log("click");
        if (currentWaypoint < path.vectorPath.Count)
        {
            currentWaypoint++;
        }
    }

    void TryMove()
    {
        if (currentWaypoint != 0)
        {
            this.transform.position = new Vector2(path.vectorPath[currentWaypoint - 1].x, path.vectorPath[currentWaypoint - 1].y);
        }
        Debug.Log("Distance is " + Vector2.Distance(this.transform.position, DEBUGOBJECT.transform.position));
    }

    Path RequestPath()
    {
        //Debug.Log("New Path");
        currentWaypoint = 2;
        return GetComponent<Seeker>().StartPath(transform.position, target.position);
    }

    void SetAngle()
    {
        Vector2 target= new Vector2(path.vectorPath[currentWaypoint].x, path.vectorPath[currentWaypoint].y);
        Vector2 mPos = new Vector2(this.transform.position.x, this.transform.position.y);
        float angle = Mathf.Atan2((target.y-mPos.y),(target.x-mPos.x)) * Mathf.Rad2Deg;
        this.transform.rotation = Quaternion.Euler(0,0,angle);
        //Debug.Log("Trying to set angle, is this correct? " + angle);
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
            yield return new WaitForSeconds(repathRate);
        }
    }

}
