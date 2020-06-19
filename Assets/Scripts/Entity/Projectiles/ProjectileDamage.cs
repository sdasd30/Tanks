using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(BoxCollider2D))]

public class ProjectileDamage : MonoBehaviour
{
    // Start is called before the first frame update

    ProjectileStats mStats;
    float mDamage;
    GameObject mGod;
    GameObject createOnDeath;
    bool friendly;
    private void Start()
    {
        mStats = GetComponent<ProjectileStats>();
        mDamage = mStats.damage;
        mGod = mStats.creator;
        createOnDeath = mStats.creation;
        friendly = mStats.friendly;
    }

    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject other = collision.gameObject;
        if (other.GetComponent<Collider2D>().isTrigger || other == mGod)
        {
            return;
        }

        if (other.GetComponent<Attackable>() == null)
        {
            DestroyManager();
            return;
        }
        else
        {
            other.GetComponent<Attackable>().changeHealth(-mDamage);
            DestroyManager();
            return;
        }

    }*/

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject other = collision.gameObject;
        if (other.GetComponent<Collider2D>().isTrigger || other == mGod)
        {
            //Debug.Log("I either collided into my master or a peer. Ignoring collision");
            return;
        }

        if (other.GetComponent<Attackable>() == null)
        {
            DestroyManager();
            return;
        }
        else
        {
            //Debug.Log("I hit a thing to hurt");
            if (other.GetComponent<Attackable>().getTeam() != friendly)
            {
                other.GetComponent<Attackable>().changeHealth(-mDamage);
                DestroyManager();
                return;
            }
        }
    }

    private void DestroyManager()
    {
        if (createOnDeath != null)
        {
            Instantiate(createOnDeath, transform.position, transform.rotation);
        }
        //Debug.Log("trying to destroy self");
        Destroy(this.gameObject);
    }
}
