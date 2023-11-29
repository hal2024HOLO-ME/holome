using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExfrowerModel : MonoBehaviour
{
    private new GameObject gameObject;

    private void Awake()
    {
        gameObject = GameObject.Find("exfrower");
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }
}
