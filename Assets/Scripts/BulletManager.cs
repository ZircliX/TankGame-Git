using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{
    [SerializeField] internal BulletType bulletData;

    private void OnCollisionEnter(Collision other)
    {
    }
}
