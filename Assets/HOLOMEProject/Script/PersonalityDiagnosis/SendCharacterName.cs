using MixedReality.Toolkit.UX;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SendCharacterName : MonoBehaviour
{
    public MRTKUGUIInputField characterNameInputField;
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
    private static void LoadConfig()
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
    /// �p�[�g�i�[�̖��O��o�^���鏈��
    /// </summary>
    public void HandleCharacterNameRegister()
    {
        string characterName = characterNameInputField.text;

        StartCoroutine(SendCharacterNameRequest(characterName, Login.session, PersonalityDiagnosisRadio.answerCount));
    }


    /// <summary>
    /// �L�����N�^�[�̖��O���󂯎��BE�ɑ��M����
    /// </summary>
    /// <param name="characterName"></param>
    /// <param name="session"></param>
    /// <param name="answerCount"></param>
    /// <returns></returns>
    private IEnumerator SendCharacterNameRequest(string characterName , string session, int answerCount)
    {
        WWWForm form = new WWWForm();
        form.AddField("user_id", session);
        form.AddField("character_id", answerCount);
        form.AddField("character_name", characterName);
        form.AddField("character_type", SelectCharacterType.characterType);
        form.AddField("character_file_name", new SendResult().GetResponseFileName());

        // HACK: DB�ւ�update�Ȃ̂�POST�g���Ă�Bwww�̎d�l�I�Ɏ����Ɏ��Ԃ�����
        using (UnityWebRequest www = UnityWebRequest.Post(config.BASE_URL + "/character/register", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                HandleSuccessfulCharacterNameRegister(www.downloadHandler.text);
            }
        }
    }

    /// <summary>
    /// TODO:BE����̃��X�|���X���n���h�����O����
    /// </summary>
    /// <param name="response"></param>
    private void HandleSuccessfulCharacterNameRegister(string response)
    {
        Debug.Log(response);
    }
}