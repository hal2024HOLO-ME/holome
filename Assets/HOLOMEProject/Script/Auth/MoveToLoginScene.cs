using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToLoginScene : MonoBehaviour
{
    /// <summary>
    /// �T�C���C���p�̃V�[���Ɉړ�
    /// </summary>
    public void MoveToLogin()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("LoginScene");
    }
}
