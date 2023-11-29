using MixedReality.Toolkit.UX;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;

public class Register : MonoBehaviour
{
    public MRTKUGUIInputField emailInputField;
    public MRTKUGUIInputField passwordInputField;
    public MRTKUGUIInputField confirmPasswordInputField;
    public TextMeshPro ErrorText;

    /// <summary>
    /// 会員登録処理
    /// </summary>
    public void HandleRegister()
    {
        string email = emailInputField.text;
        string password = passwordInputField.text;
        string confirmPassword = confirmPasswordInputField.text;

        if (IsPasswordValid(password, confirmPassword))
        {
            StartCoroutine(SendRegisterRequest(email, password));
        }
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
    /// すでに登録済みのメールアドレスは使用不可
    /// </summary>
    /// <param name="email"></param>
    /// <param name="password"></param>
    /// <returns></returns>

    private IEnumerator SendRegisterRequest(string email, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("email", email);
        form.AddField("password", password);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost:3001/api/v1/auth/signup", form))
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
        string errorType = error.type;
        if (errorType.Equals("validation"))
        {
            ErrorText.text = "このメールアドレスは既に登録されています";
        }
    }
}
