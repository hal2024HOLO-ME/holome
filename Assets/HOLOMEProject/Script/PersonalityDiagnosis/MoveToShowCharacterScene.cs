using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToShowCharacterScene : MonoBehaviour
{

    /// <summary>
    /// �L�����N�^�[��\���p�̃V�[���Ɉړ�
    /// </summary>
    public void MoveToShowCharacter()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("ShowCharacterScene");
    }
}
