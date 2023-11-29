using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MixedReality.Toolkit.UX;

public class SelectCharacterType : MonoBehaviour
{
    public ToggleCollection toggleCollection;
    public static int characterType;

    /// <summary>
    /// �L�����N�^�[�̃^�C�v��I������
    /// �u0�v�FNormal�^�C�v
    /// �u1�v�FGhost�^�C�v
    /// </summary>
    public void OnToggleClick()
    {
        characterType = toggleCollection.CurrentIndex;
    }
}
