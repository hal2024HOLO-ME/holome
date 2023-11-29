using MixedReality.Toolkit.UX;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HidePassword : MonoBehaviour
{
    public MRTKUGUIInputField passwordInputField;

    void Start()
    {
        passwordInputField.contentType = MRTKUGUIInputField.ContentType.Password;
    }
}
