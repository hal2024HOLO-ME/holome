using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using MixedReality.Toolkit.UX;

public class Login : MonoBehaviour
{
    public MRTKUGUIInputField emailInputField;
    public MRTKUGUIInputField passwordInputField;
    public static string session;

    /// <summary>
    /// ���O�C������
    /// </summary>
    public void HandleLogin()
    {
        string email = emailInputField.text;
        string password = passwordInputField.text;

        StartCoroutine(SendLoginRequest(email, password));
    }

    /// <summary>
    /// ���[���A�h���X�ƃp�X���[�h���󂯎��BE�ŏƍ�����
    /// </summary>
    /// <param name="email"></param>
    /// <param name="password"></param>
    /// <returns></returns>

    private IEnumerator SendLoginRequest(string email, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("email", email);
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
    /// BE����̃��X�|���X���n���h�����O����
    /// </summary>
    /// <param name="response"></param>
    private void HandleSuccessfulLogin(string response)
    {
        session = response;
        if (!string.IsNullOrEmpty(session))
        {
            Debug.Log("session���擾�ł���");
            UnityEngine.SceneManagement.SceneManager.LoadScene("TopScene");
        }
        else
        {
            Debug.LogError("session���擾�ł��Ȃ�����");
        }
    }
}
