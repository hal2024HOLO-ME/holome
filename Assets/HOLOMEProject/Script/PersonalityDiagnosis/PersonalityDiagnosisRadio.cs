using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using MixedReality.Toolkit.UX;
using UnityEngine;


public class PersonalityDiagnosisRadio : MonoBehaviour
{
    public ToggleCollection toggleCollection;
    public static int answerCount;

    /// <summary>
    /// 性格診断のYes、Noによってカウントを替える。
    /// 以下のカウントで表示するキャラクターを切り替える
    /// 0：ねこ
    /// 1、2：いぬ
    /// 3：たぬき
    /// 4：きつね
    /// 5：Mii
    /// </summary>
    public void OnToggleClick()
    {
        if (toggleCollection.CurrentIndex == 0)
        {
            answerCount++;
            Debug.Log(answerCount);
        }
        else if (toggleCollection.CurrentIndex == 1)
        {
            answerCount--;
            Debug.Log(answerCount);
        }
    }
}
