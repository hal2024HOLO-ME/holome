using MixedReality.Toolkit.UX;
using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;

public class Register : MonoBehaviour
{
    public MRTKUGUIInputField userIdInputField;
    public MRTKUGUIInputField passwordInputField;
    public MRTKUGUIInputField confirmPasswordInputField;
    public TextMeshPro ErrorText;

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
    /// 会員登録処理
    /// </summary>
    public void HandleRegister()
    {
        string userId = userIdInputField.text;
        string password = passwordInputField.text;
        string confirmPassword = confirmPasswordInputField.text;

        if(IsUserIdValid(userId) && IsPasswordValid(password, confirmPassword))
        {
            StartCoroutine(SendRegisterRequest(userId, password));
        }
    }

    /// <summary>
    /// ユーザーIDのバリデーション
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    private bool IsUserIdValid(string userId)
    {
        if(userId == null)
        {
            ErrorText.text = "ユーザーIDを入力してください";
            return false;
        }
        else if (userId.Length < 6 || 20 < userId.Length)
        {
            ErrorText.text = "ユーザーIDは6文字以上20字以下の半角英数字である必要があります";
            return false;
        }
        return true;
    }


    /// <summary>
    /// パスワードのバリデーション
    /// </summary>
    /// <param name="password"></param>
    /// <param name="confirmPassword"></param>
    /// <returns></returns>
    private bool IsPasswordValid(string password, string confirmPassword)
    {
        if (password.Length < 8 || password.Length > 32)
        {
            ErrorText.text = "パスワードは8文字以上32文字以下である必要があります";
            return false;
        }
        else if (password != confirmPassword)
        {
            ErrorText.text = "パスワードが一致しません";
            return false;
        }
        return true;
    }

    /// <summary>
    /// すでに登録済みのユーザーIDは使用不可
    /// </summary>
    /// <param name="userId">ログイン用ユーザーID</param>
    /// <param name="password">パスワード</param>
    /// <returns></returns>

    private IEnumerator SendRegisterRequest(string userId, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("login_id", userId);
        form.AddField("password", password);

        using (UnityWebRequest www = UnityWebRequest.Post(config.BASE_URL + "/auth/signup", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                HandleError(www.downloadHandler.text);
            }
            else
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("LoginScene");
            }
        }
    }

    /// <summary>
    /// BEからのエラーハンドリング
    /// </summary>
    /// <param name="jsonResponse"></param>

    private void HandleError(string jsonResponse)
    {
        ErrorType error = JsonUtility.FromJson<ErrorType>(jsonResponse);
        Debug.Log(error);
        string errorType = error.type;
        if (errorType.Equals("validation"))
        {
            ErrorText.text = "このユーザーIDは既に登録されています";
        }
    }
}