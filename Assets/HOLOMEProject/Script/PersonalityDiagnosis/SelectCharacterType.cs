using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MixedReality.Toolkit.UX;

public class SelectCharacterType : MonoBehaviour
{
    public ToggleCollection toggleCollection;
    public static int characterType;

    /// <summary>
    /// キャラクターのタイプを選択する
    /// 「0」：Normalタイプ
    /// 「1」：Ghostタイプ
    /// </summary>
    public void OnToggleClick()
    {
        characterType = toggleCollection.CurrentIndex;
    }
}
