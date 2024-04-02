using System;
using System.Collections;
using UnityEngine;

public static class StaticResetBool
{
    public static IEnumerator ResetBool(Action<bool> myBool, float timeDelay)
    {
        myBool(false);
        yield return new WaitForSeconds(timeDelay);
        myBool(true);
    }
}