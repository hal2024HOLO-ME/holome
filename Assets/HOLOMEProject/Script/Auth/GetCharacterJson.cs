using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.Networking;

public class GetCharacterJson : MonoBehaviour
{
    private static Config config;
    private string responseString;

    /// <summary>
    ///  �N������LoadConfig()��config�t�@�C����ǂݍ���
    /// </summary>
    void Awake()
    {
        if (config == null)
        {
            LoadConfig();
        }
        HandleGetCharacterData();
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
    /// BE����p�[�g�i�[�̃f�[�^�����X�|���X�Ƃ��Ď󂯎��
    /// </summary>
    public IEnumerator GetCharacterData(string user_id)
    {
        WWWForm form = new WWWForm();
        Debug.Log("session" + user_id);
        form.AddField("user_id", user_id);
        using (UnityWebRequest www = UnityWebRequest.Post(config.BASE_URL + "/character/get-json", form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
                responseString = www.downloadHandler.text; // ���X�|���X�𕶎���Ƃ��Ď擾
                Debug.Log(responseString); // ���X�|���X�����O�ɏo��
            }
        }
    }

    /// <summary>
    /// �T�C���A�E�g����
    /// </summary>
    public void HandleGetCharacterData()
    {
        StartCoroutine(GetCharacterData(Login.session));
    }


}
