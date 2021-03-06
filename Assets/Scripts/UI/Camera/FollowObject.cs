﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    public Transform Target;
    public Transform OptionalFollowRot;
    public bool isCamera;
    void Start()
    {
        if (isCamera)
        {
            Target = FindObjectOfType<PlayerInputBody>().transform;
        }
    }

    void LateUpdate()
    {
        if (Target != null) {
           transform.position = new Vector3(Target.position.x, Target.position.y, Target.position.z - 5);
           if (OptionalFollowRot != null)
               transform.eulerAngles = new Vector3(0f, 0f, OptionalFollowRot.eulerAngles.z - 90);
        }
    }
}
