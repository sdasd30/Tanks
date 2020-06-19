using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireProjectile : MonoBehaviour
{
    [SerializeField] float cooldownMax;
    [SerializeField] float curCooldown;
    [SerializeField] bool playerControl;
    public bool autoLoader;
    InputPacket ip;
    PlayerInputTurretAction1 playerAction1;
    GunStats gs;
    Vector2 offset;

    // Start is called before the first frame update
    void Start()
    {
        ip = new InputPacket();
        if (playerControl)
            playerAction1 = GetComponent<PlayerInputTurretAction1>();
        gs = GetComponent<GunStats>();
        cooldownMax = gs.cooldown;
        offset = gs.offset;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerControl)
        {
            ip = playerAction1.GetInputPacket(ip);
        }

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
