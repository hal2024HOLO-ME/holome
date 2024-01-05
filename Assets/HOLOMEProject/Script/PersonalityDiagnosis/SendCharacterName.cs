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
    ///  起動時にLoadConfig()でconfigファイルを読み込む
    /// </summary>
    void Awake()
    {
        if (config == null)
        {
            LoadConfig();
        }
    }

    /// <summary>
    /// ResourcesにあるConfigファイルをロードする
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
            Debug.LogError("Configファイルが見つかりません！");
        }
    }

    /// <summary>
    /// パートナーの名前を登録する処理
    /// </summary>
    public void HandleCharacterNameRegister()
    {
        string characterName = characterNameInputField.text;

        StartCoroutine(SendCharacterNameRequest(characterName, Login.session, PersonalityDiagnosisRadio.answerCount));
    }


    /// <summary>
    /// キャラクターの名前を受け取りBEに送信する
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

        // HACK: DBへのupdateなのにPOST使ってる。wwwの仕様的に実装に時間かかる
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
    /// TODO:BEからのレスポンスをハンドリングする
    /// </summary>
    /// <param name="response"></param>
    private void HandleSuccessfulCharacterNameRegister(string response)
    {
        Debug.Log(response);
    }
}