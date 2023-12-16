using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeToCharacterNameSlate : MonoBehaviour
{
    public GameObject personalityDiagnosisResultSlate;
    public GameObject characterNameSlate;

    /// <summary>
    /// 性格診断結果表示用のパネルからキャラクターの名前を登録するパネルに切り替える
    /// </summary>
    public void ChangeToCharacterNameSlatePanel()
    {
        personalityDiagnosisResultSlate.SetActive(false);
        characterNameSlate.SetActive(true);
    }
}
