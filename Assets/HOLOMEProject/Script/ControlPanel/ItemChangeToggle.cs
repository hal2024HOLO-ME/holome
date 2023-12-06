using System.Collections;
using System.Collections.Generic;
using MixedReality.Toolkit.UX;
using UnityEngine;

public class ItemChangeToggle : MonoBehaviour
{
    public ToggleCollection toggleCollection;
    public GameObject NeckParts;
    public GameObject HeadParts;
    public GameObject FaceParts;

    /// <summary>
    /// ラジオボタンを選択することでアイテムの種類変える
    /// </summary>
    public void OnToggleClick()
    {
        if (toggleCollection.CurrentIndex == 0)
        {
            NeckParts.SetActive(true);
            HeadParts.SetActive(false);
            FaceParts.SetActive(false);
        }
        else if (toggleCollection.CurrentIndex == 1)
        {
            NeckParts.SetActive(false);
            HeadParts.SetActive(true);
            FaceParts.SetActive(false);
        }
        else if (toggleCollection.CurrentIndex == 2)
        {
            NeckParts.SetActive(false);
            HeadParts.SetActive(false);
            FaceParts.SetActive(true);
        }
    }

}
