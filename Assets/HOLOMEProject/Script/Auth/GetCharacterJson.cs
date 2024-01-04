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
    ///  起動時にLoadConfig()でconfigファイルを読み込む
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
    /// ResourcesにあるConfigファイルをロードする
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
            Debug.LogError("Configファイルが見つかりません！");
        }
    }

    /// <summary>
    /// BEからパートナーのデータをレスポンスとして受け取る
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
                responseString = www.downloadHandler.text; // レスポンスを文字列として取得
                Debug.Log(responseString); // レスポンスをログに出力
            }
        }
    }

    /// <summary>
    /// サインアウト処理
    /// </summary>
    public void HandleGetCharacterData()
    {
        StartCoroutine(GetCharacterData(Login.session));
    }


}
