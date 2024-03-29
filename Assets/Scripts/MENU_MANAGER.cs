using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MENU_MANAGER : MonoBehaviour
{
    public static MENU_MANAGER Instance;
    
    public GameObject playerDead;

    private void Awake()
    {
        Instance = this;
    }
}
