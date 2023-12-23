using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// userIdとキャラクターを持っているかのフラグ
[System.Serializable]
public class LoginResponse
{
    public string id;
    public Boolean isCharacterExists;
}
