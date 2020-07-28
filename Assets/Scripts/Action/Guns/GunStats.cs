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
    public bool preignitor = false;
    public bool autoLoader = false;
    public Vector2 offset = new Vector2 (0f,0f);
    public GameObject CreateOnProjectileDeath;


    public float maxRotSpeed = 180;
    public float accelRotSpeed = 360;
    public float dampenRotSpeed = 2;
}

