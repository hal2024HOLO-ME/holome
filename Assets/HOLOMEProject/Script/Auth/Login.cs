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
        string userId = userIdInputField.text;
        string password = passwordInputField.text;

        StartCoroutine(SendLoginRequest(userId, password));
    }

    /// <summary>
    /// ���[�U�[ID�ƃp�X���[�h���󂯎��BE�ŏƍ�����
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="password"></param>
    /// <returns></returns>

    private IEnumerator SendLoginRequest(string userId, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("login_id", userId);
        form.AddField("password", password);

        // NOTE: ���[�J����BE���N�����Ă���ꍇ�͉��L��URL���g�p����
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
    /// BE����̃��X�|���X���n���h�����O����
    /// ���O�C���������A�L�����N�^�[�����łɎ����Ă�����ShowCharacterScene�ɑJ��
    /// �L�����N�^�[�����Ȃ��Ȃ�TopScene�ɑJ�ڂ����i�f�f����
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
                Debug.Log("�L�����N�^�[���Ȃ�");
                UnityEngine.SceneManagement.SceneManager.LoadScene("TopScene");
            }
        }
        else
        {
            Debug.LogError("session���擾�ł��Ȃ�����");
        }
    }
}
