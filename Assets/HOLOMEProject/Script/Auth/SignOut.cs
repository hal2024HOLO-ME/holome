using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SignOut : MonoBehaviour
{
    private static Config config;

    private CharacterModel characterModel;

    /// JSON作成用変数
    private int nostalgicLevel;
    private Color eyeColor;
    private Color earColor;
    private Color bodyColor;
    private string neckObjectName;
    private string headObjectName;
    private string faceObjectName;

    /// <summary>
    ///  起動時にLoadConfig()でconfigファイルを読み込む
    /// </summary>
    void Awake()
    {
        if (config == null)
        {
            LoadConfig();

            characterModel = gameObject.GetComponent<CharacterModel>();

            this.nostalgicLevel = 0;
            this.neckObjectName = null;
            this.headObjectName = null;
            this.faceObjectName = null;
        }
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
    /// パートナーのJSONデータをBEに送信する
    /// </summary>
    public IEnumerator PostCharacterData(string url, string jsonString)
    {
        WWWForm form = new WWWForm();
        Debug.Log("session"+ Login.session);
        form.AddField("login_id", Login.session);
        form.AddField("character_name", new SendResult().GetResponseFileName());

        form.AddField("json_data", jsonString);

        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
            }
        }
    }

    /// <summary>
    /// サインアウト処理
    /// </summary>
    public void HandleSignOut()
    {
        characterModel = gameObject.GetComponent<CharacterModel>();

        this.neckObjectName = DecorationsController.neckTargetName;
        this.headObjectName = DecorationsController.headTargetName;
        this.faceObjectName = DecorationsController.faceTargetName;

        this.earColor = ColorChanger.earColor;
        this.eyeColor = ColorChanger.eyeColor;
        this.bodyColor = ColorChanger.bodyColor;
        Debug.Log(ColorChanger.earColor);
        Debug.Log(ColorChanger.eyeColor);
        Debug.Log(ColorChanger.bodyColor);

        WWWForm form = new();
        CharacterData characterData = new CharacterData();
        characterData.nostalgic_level = this.nostalgicLevel;
        characterData.color = new PartnerColor { eye = this.eyeColor, ear = this.earColor, body = this.bodyColor };
        characterData.customize = new Customize { neck = this.neckObjectName, head = this.headObjectName, face = this.faceObjectName };

        string jsonString = JsonUtility.ToJson(characterData);
        Debug.Log("characterData" + characterData);
        Debug.Log("json" + jsonString);

        StartCoroutine(PostCharacterData(config.BASE_URL + "/auth/signout", jsonString));
        Login.session = null;
        UnityEngine.SceneManagement.SceneManager.LoadScene("LoginScene");
    }
}