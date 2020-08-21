using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public enum MovementState
{
    STILL,
    ROTATE,
    MOVE,
    FULL
}

public class AITank : AIScript
{


    MovementState state = MovementState.STILL;
    //public float stopDistance; //When will the AI stop moving towards the target?
    public int waypointsAway = 5; //How many waypoints away should the AI stop trying to get to the playeR?
    Path path;
    public Transform target; //Defaults to the Player
    public float distBuffer = .01f; //How far can this get from the node while being okay?
    public float angleBuffer = 3f; //How far can it be the wrong angle before it says, OK!
    public float repathRate = .3f; //How much time should pass between path finds?
    Rigidbody2D mRigidBody;
    int currentWaypoint = 2;
    InputPacket ip;

    Transform DEBUGOBJECT;

    private void Start()
    {
        ip = new InputPacket();
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
        if (Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]) < distBuffer)
        {
            IncrementCurWaypoint();
        }
        //DEBUGOBJECT.position = path.vectorPath[currentWaypoint];
        //SetAngle();
        CheckState();
        ActionCall();
        if (Input.GetKeyDown("j")) //DEBUG
        {
            IncrementCurWaypoint();
            TryMove();
        }
    }


    void CheckState()
    {
        if (path.vectorPath.Count - currentWaypoint < waypointsAway)
        {
            state = MovementState.STILL;
        }
        else if (CheckAngleDiff() >= angleBuffer)
        {
            state = MovementState.ROTATE;
        }
        else if (CheckAngleDiff() >= angleBuffer/2f)
        {
            state = MovementState.FULL;
        }
        else
        {
            state = MovementState.MOVE;
        }
        /*else if (Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]) > distBuffer)
        {
            state = MovementState.ROTATE;
        }
        else
        {
            state = MovementState.STILL;
        }*/
    }

    void ActionCall()
    {
        if (state == MovementState.STILL)
        {
            ip.inputRotate = 0;
            ip.inputTranslate = 0;
        }
        if (state == MovementState.MOVE)
        {
            ip.inputRotate = 0;
            ip.inputTranslate = 1;
        }
        if (state == MovementState.ROTATE)
        {
            ip.inputTranslate = 0;
            ip.inputRotate = CalcAngle(path.vectorPath[currentWaypoint],this.transform.position);
        }
        if (state == MovementState.FULL)
        {
            ip.inputRotate = CalcAngle(path.vectorPath[currentWaypoint], this.transform.position);
            ip.inputTranslate = 1;
        }

    }
    public void IncrementCurWaypoint()
    {
        if (currentWaypoint< path.vectorPath.Count)
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
    }

    Path RequestPath()
    {
        currentWaypoint = 2;
        return GetComponent<Seeker>().StartPath(transform.position, target.position);
    }

    float CheckAngleDiff()
    {
        float diff = 0;
        Vector2 target= new Vector2(path.vectorPath[currentWaypoint].x, path.vectorPath[currentWaypoint].y);
        Vector2 mPos = new Vector2(this.transform.position.x, this.transform.position.y);
        float angle = Mathf.Atan2((target.y-mPos.y),(target.x-mPos.x)) * Mathf.Rad2Deg;
        //this.transform.rotation = Quaternion.Euler(0,0,angle);
        diff = Mathf.Abs((angle - this.transform.rotation.eulerAngles.z)%360);
        //Debug.Log("Personally, I think the diffrence is:" + diff);
        return diff;
    }
    float CalcAngle(Vector3 targetTransform,Vector3 mTransform)
    {
        //Debug.Log("Schmoving");
        Vector2 targetPosition = new Vector2(targetTransform.x - mTransform.x, targetTransform.y - mTransform.y);
        float num = 0;
        float dAn = Mathf.Atan2(targetPosition.y, targetPosition.x) * Mathf.Rad2Deg;
        float mAn = mRigidBody.rotation;
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

        if (dAn - mAn > 0)
        {
            //Debug.Log("ip1");
            num = 1 * coolBool;

        }
        else if (mAn - dAn >= 0)
        {
            //Debug.Log("ip-1");
            num = -1 * coolBool;
        }
        return num;
    }
    public override InputPacket GetInputPacket(InputPacket nIP)
    {
        nIP = ip;
        return nIP;
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
