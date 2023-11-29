using System;
using UnityEngine;

public class DateTimeWrapper : MonoBehaviour
{
    public DateTime DateTime { get; set; }

    public DateTime Now
    {
        get { return DateTime.Now; }
        set { DateTime = value; }
    }
}
