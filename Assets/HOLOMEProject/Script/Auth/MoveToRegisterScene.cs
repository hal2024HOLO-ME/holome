using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToRegisterScene : MonoBehaviour
{
    /// <summary>
    /// サインアップ用のシーンに移動
    /// </summary>
    public void MoveToRegister()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("RegisterScene");
    }
}
