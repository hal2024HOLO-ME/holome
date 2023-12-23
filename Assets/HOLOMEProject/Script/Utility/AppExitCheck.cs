using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppExitCheck : MonoBehaviour
{
    /// <summary>
    /// フォーカスの変更イベント
    /// </summary>
    /// <param name="focus"></param>
    private void OnApplicationFocus(bool focus)
    {
        // フォーカスが失われた場合はアプリを自動で完全終了する
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