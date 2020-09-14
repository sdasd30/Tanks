using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemyTurretRaycast : MonoBehaviour
{
    public LayerMask IgnoreMe;
    public GameObject target;
    public bool aimAtPlayer = true;
    Vector2 direction;
    Vector2 mPos;
    float range;
    RaycastHit2D hit;
    // Start is called before the first frame update
    void Start()
    { 
        range = GetComponent<GunStats>().AIRange;
        Debug.Log(range);
        if (aimAtPlayer)
        {
            target = FindObjectOfType<PlayerInputBody>().gameObject;
        }
    }

    private void Update()
    {
        //Debug.Log("yo dude this is hit");
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        direction = target.transform.position - this.transform.position;
        mPos = new Vector2(this.transform.position.x, this.transform.position.y);

        hit = Physics2D.Raycast(mPos,direction, range, ~IgnoreMe);
        //Debug.DrawRay(mPos,direction);
        Debug.DrawLine(mPos, hit.point, Color.red);

        Debug.Log(hit.point);
        //Debug.Log(hit.rigidbody.gameObject);
        if (hit.rigidbody != null && hit.rigidbody == target.GetComponent<Rigidbody2D>())
        {
            Debug.Log("Player hit");
        }
    }
}
