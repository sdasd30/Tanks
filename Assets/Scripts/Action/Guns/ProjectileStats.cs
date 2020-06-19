using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileStats : MonoBehaviour
{
    [HideInInspector] public float moveSpeed;
    [HideInInspector] public bool colliding;
    [HideInInspector] public bool friendly;
    [HideInInspector] public float damage;
    [HideInInspector] public float bulletLifetime;
    [HideInInspector] public GameObject creator;
    [HideInInspector] public GameObject creation;

    public void setMyStats(GunStats gs, GameObject mCreator)
    {
        moveSpeed = gs.moveSpeed;
        colliding = gs.colliding;
        friendly = gs.friendly;
        damage = gs.damage;
        bulletLifetime = gs.bulletLifespan;
        creator = mCreator;
        creation = gs.CreateOnProjectileDeath;
    }
}
