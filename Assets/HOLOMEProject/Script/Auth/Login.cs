using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using MixedReality.Toolkit.UX;

public class Login : MonoBehaviour
{
    public MRTKUGUIInputField userIdInputField;
    public MRTKUGUIInputField passwordInputField;
    public static string session;
    private static string responseSession;

    private static Config config;

    public string GetResponseSession()
    {
        return responseSession;
    }


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
    /// ログイン処理
    /// </summary>
    public void HandleLogin()
    {
        string userId = userIdInputField.text;
        string password = passwordInputField.text;

        StartCoroutine(SendLoginRequest(userId, password));
    }

    /// <summary>
    /// ユーザーIDとパスワードを受け取りBEで照合する
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="password"></param>
    /// <returns></returns>

    private IEnumerator SendLoginRequest(string userId, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("login_id", userId);
        form.AddField("password", password);

        // NOTE: ローカルでBEを起動している場合は下記のURLを使用する
        // using (UnityWebRequest www = UnityWebRequest.Post("http://localhost:3001/api/v1/auth/signin", form))
        using (UnityWebRequest www = UnityWebRequest.Post(config.BASE_URL + "/auth/signin", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
                HandleSuccessfulLogin(www.downloadHandler.text);
            }
        }
    }

    /// <summary>
    /// BEからのレスポンスをハンドリングする
    /// ログイン成功時、キャラクターをすでに持っていたらShowCharacterSceneに遷移
    /// キャラクターがいないならTopSceneに遷移し性格診断する
    /// </summary>
    /// <param name="response"></param>
    private void HandleSuccessfulLogin(string response)
    {
        responseSession = response;
        var sessionObject = JsonUtility.FromJson<LoginResponse>(responseSession);

        session = sessionObject.id;

        if (sessionObject != null)
        {
            if (sessionObject.isCharacterExists)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("ShowCharacterScene");
            }
            else
            {
                Debug.Log("キャラクターがない");
                UnityEngine.SceneManagement.SceneManager.LoadScene("TopScene");
            }
        }
        else
        {
            Debug.LogError("sessionが取得できなかった");
        }
    }
}
