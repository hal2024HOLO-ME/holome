using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowShower : MonoBehaviour
{
    public GameObject Shower;
    public void OnClickShowerButton()
    {
        Shower.SetActive(true);
    }
}
