using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPhotonScene : MonoBehaviour
{
    /// <summary>
    /// Photon��\���p�̃V�[���Ɉړ�
    /// TODO: ���\�p�Ɉꎞ�I��ShowCharacterScene��ς���B�C����#55�őΉ�
    /// </summary>
    public void MoveToPhotonCharacter()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("PhotonTest");
    }
}
