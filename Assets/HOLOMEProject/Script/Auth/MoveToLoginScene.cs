using UnityEngine;

public class MoveToLoginScene : MonoBehaviour
{
    /// <summary>
    /// サインイン用のシーンに移動
    /// </summary>
    public void MoveToLogin()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("LoginScene");
    }
}
