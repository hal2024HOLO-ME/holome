using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPhotonScene : MonoBehaviour
{
    /// <summary>
    /// Photonを表示用のシーンに移動
    /// TODO: 発表用に一時的にShowCharacterSceneを変える。修正は#55で対応
    /// </summary>
    public void MoveToPhotonCharacter()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("PhotonTest");
    }
}
