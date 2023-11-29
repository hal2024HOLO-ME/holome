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
    /// ���i�f�f��Yes�ANo�ɂ���ăJ�E���g��ւ���B
    /// �ȉ��̃J�E���g�ŕ\������L�����N�^�[��؂�ւ���
    /// 0�F�˂�
    /// 1�A2�F����
    /// 3�F���ʂ�
    /// 4�F����
    /// 5�FMii
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
