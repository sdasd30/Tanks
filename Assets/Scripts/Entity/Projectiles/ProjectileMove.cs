using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class ProjectileMove : MonoBehaviour
{
    Rigidbody2D mBody;
    float speed;
    float angle;
    float xvel, yvel;
    float lifespan;
    float lived;
    public void Start()
    {
        mBody = GetComponent<Rigidbody2D>();
        speed = GetComponent<ProjectileStats>().moveSpeed;
        angle = mBody.rotation;
        lifespan = GetComponent<ProjectileStats>().bulletLifetime;
    }

    void FixedUpdate()
    {
        xvel = (Mathf.Rad2Deg * Time.fixedDeltaTime * Mathf.Cos(angle * Mathf.Deg2Rad)) * speed;
        yvel = (Mathf.Rad2Deg * Time.fixedDeltaTime * Mathf.Sin(angle * Mathf.Deg2Rad)) * speed;
        mBody.velocity = new Vector2(xvel, yvel);
    }

    private void Update()
    {
        lived += Time.deltaTime;
        if (lived >= lifespan)
        {
            Destroy(this.gameObject);
        }
    }
}
