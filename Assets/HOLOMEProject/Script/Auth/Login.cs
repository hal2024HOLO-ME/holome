using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using MixedReality.Toolkit.UX;

public class Login : MonoBehaviour
{
    public MRTKUGUIInputField emailInputField;
    public MRTKUGUIInputField passwordInputField;
    public static string session;

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
        form.AddField("login_id", email);
        form.AddField("password", password);

        using (UnityWebRequest www = UnityWebRequest.Post(config.BASE_URL + "/auth/signin", form))
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
