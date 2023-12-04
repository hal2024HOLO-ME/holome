using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using MixedReality.Toolkit.UX;

public class Login : MonoBehaviour
{
    public MRTKUGUIInputField loginIdInputField;
    public MRTKUGUIInputField passwordInputField;
    public static string session;

    /// <summary>
    /// ログイン処理
    /// </summary>
    public void HandleLogin()
    {
        string loginId = loginIdInputField.text;
        string password = passwordInputField.text;

        StartCoroutine(SendLoginRequest(loginId, password));
    }

    /// <summary>
    /// メールアドレスとパスワードを受け取りBEで照合する
    /// </summary>
    /// <param name="loginId"></param>
    /// <param name="password"></param>
    /// <returns></returns>

    private IEnumerator SendLoginRequest(string loginId, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("login_id", loginId);
        form.AddField("password", password);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost:3001/api/v1/auth/signin", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                HandleSuccessfulLogin(www.downloadHandler.text);
            }
        }
    }

    /// <summary>
    /// BEからのレスポンスをハンドリングする
    /// </summary>
    /// <param name="response"></param>
    private void HandleSuccessfulLogin(string response)
    {
        session = response;
        if (!string.IsNullOrEmpty(session))
        {
            Debug.Log("sessionが取得できた");
            UnityEngine.SceneManagement.SceneManager.LoadScene("TopScene");
        }
        else
        {
            Debug.LogError("sessionが取得できなかった");
        }
    }
}
