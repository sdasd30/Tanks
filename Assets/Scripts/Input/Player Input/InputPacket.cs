using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputPacket
{
    public float inputTranslate;
    public float inputRotate;
    public float inputTurret;
    public bool cancelTranslate;
    public bool cancelRotate;
    public bool inputReload;
    public bool inputAction1;

    public void reset()
    {
    cancelTranslate = false;
    cancelRotate = false;
    }
}
