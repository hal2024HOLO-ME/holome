using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeToCharacterNameSlate : MonoBehaviour
{
    public GameObject personalityDiagnosisResultSlate;
    public GameObject characterNameSlate;

    /// <summary>
    /// ���i�f�f���ʕ\���p�̃p�l������L�����N�^�[�̖��O��o�^����p�l���ɐ؂�ւ���
    /// </summary>
    public void ChangeToCharacterNameSlatePanel()
    {
        personalityDiagnosisResultSlate.SetActive(false);
        characterNameSlate.SetActive(true);
    }
}
