using System.Collections;
using System.Collections.Generic;
using MixedReality.Toolkit.UX;
using UnityEngine;

public class ChangeTarget : MonoBehaviour
{
     public ToggleCollection toggleCollection;
     private static string[] target = new string[2];

    public void OnToggleClick()
    {
        if (toggleCollection.CurrentIndex == 0)
        {
            target[0] = "eye";
            target[1] = null;
        }
        else if (toggleCollection.CurrentIndex == 1)
        {
            target[0] = "ear";
            target[1] = "pattern";
        }
        else if (toggleCollection.CurrentIndex == 2)
        {
            target[0] = "body";
            target[1] = "head";
        }
    }

    public string[] GetTarget(){
        return target;
    }

    public string GetTarget1(){
        return target[0];
    }
}
