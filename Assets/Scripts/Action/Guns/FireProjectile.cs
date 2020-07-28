using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireProjectile : MonoBehaviour
{
    float cooldownMax;
    float curCooldown;
    bool playerControl;
    bool autoLoader;
    InputPacket ip;
    GunStats gs;
    Vector2 offset;

    // Start is called before the first frame update
    void Start()
    {
        ip = new InputPacket();
        gs = GetComponent<GunStats>();
        cooldownMax = gs.cooldown;
        if (gs.preignitor == false)
            curCooldown = cooldownMax;
        autoLoader = gs.autoLoader;
        offset = gs.offset;
    }

    // Update is called once per frame
    public void SendInputPacket(InputPacket nIP)
    {
        ip = nIP;
    }

    void Update()
    {
        if (!autoLoader)
        {
            if (ip.inputReload && curCooldown >= 0)
            {
                curCooldown -= Time.deltaTime;
            }
            else if (ip.inputAction1 && curCooldown <= 0)
            {
                float angle = GetComponent<Rigidbody2D>().rotation;
                //Debug.Log("sound:" + angle);
                float ofx = offset.x * Mathf.Rad2Deg * Mathf.Cos(angle * Mathf.Deg2Rad) * .1f;
                float ofy = offset.y * Mathf.Rad2Deg * Mathf.Sin(angle * Mathf.Deg2Rad) * .1f;
                GameObject proj = Instantiate(gs.preferedObject, transform.position + new Vector3(ofx, ofy, 0f), transform.rotation);
                proj.GetComponent<ProjectileStats>().setMyStats(gs, this.gameObject);
                curCooldown = cooldownMax;
            }
        }
        else
        {
            if (curCooldown >= 0)
            {
                curCooldown -= Time.deltaTime;
            }

            if (ip.inputAction1 && curCooldown <= 0)
            {
                float angle = GetComponent<Rigidbody2D>().rotation;
                //Debug.Log("sound:" + angle);
                float ofx = offset.x * Mathf.Rad2Deg * Mathf.Cos(angle * Mathf.Deg2Rad) * .1f;
                float ofy = offset.y * Mathf.Rad2Deg * Mathf.Sin(angle * Mathf.Deg2Rad) * .1f;
                GameObject proj = Instantiate(gs.preferedObject, transform.position + new Vector3(ofx, ofy, 0f), transform.rotation);
                proj.GetComponent<ProjectileStats>().setMyStats(gs, this.gameObject);
                curCooldown = cooldownMax;
            }

        }
    }

    public float getCooldownMax()
    {
        return cooldownMax;
    }
    public float getCurrentCooldown()
    {
        return curCooldown;
    }

}
