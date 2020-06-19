using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunStats : MonoBehaviour
{
    public float moveSpeed = 3;
    public bool colliding = true;
    public bool friendly = false;
    public float damage = 1;
    public GameObject preferedObject;
    public float bulletLifespan = 2;
    public float cooldown = 1;
    public Vector2 offset = new Vector2 (0f,0f);
    public GameObject CreateOnProjectileDeath;
}

