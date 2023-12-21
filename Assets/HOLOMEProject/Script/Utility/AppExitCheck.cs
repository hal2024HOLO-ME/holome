using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppExitCheck : MonoBehaviour
{
    /// <summary>
    /// �t�H�[�J�X�̕ύX�C�x���g
    /// </summary>
    /// <param name="focus"></param>
    private void OnApplicationFocus(bool focus)
    {
        // �t�H�[�J�X������ꂽ�ꍇ�̓A�v���������Ŋ��S�I������
        if (focus == false)
        {
#if WINDOWS_UWP
            Windows.ApplicationModel.Core.CoreApplication.Exit();
#else
            Application.Quit();
#endif
        }
    }
}