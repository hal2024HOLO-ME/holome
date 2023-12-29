using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignOut : MonoBehaviour
{
    private static Config config;

    /// <summary>
    ///  �N������LoadConfig()��config�t�@�C����ǂݍ���
    /// </summary>
    void Awake()
    {
        if (config == null)
        {
            LoadConfig();
        }
    }

    /// <summary>
    /// Resources�ɂ���Config�t�@�C�������[�h����
    /// </summary>
    private void LoadConfig()
    {
        TextAsset configFile = Resources.Load<TextAsset>("Config");
        if (configFile != null)
        {
            config = JsonUtility.FromJson<Config>(configFile.text);
        }
        else
        {
            Debug.LogError("Config�t�@�C����������܂���I");
        }
    }

    /// <summary>
    /// �T�C���A�E�g����
    /// </summary>
    public void HandleSignOut()
    {
        Debug.Log(Login.session);
        Login.session = null;
        Debug.Log(Login.session);
        UnityEngine.SceneManagement.SceneManager.LoadScene("LoginScene");
    }

}
