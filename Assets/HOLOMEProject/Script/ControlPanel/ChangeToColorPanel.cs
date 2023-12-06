using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeToColorPanel : MonoBehaviour
{
    public GameObject ColorControlPanel;
    public GameObject BaseControlPanel;
    public GameObject ItemControlPanel;

    /// <summary>
    /// カラーカスタマイズパネルを非表示にする
    /// NOTO: デフォル
    void Start()
    {
        ColorControlPanel.SetActive(false);
    }

    /// <summary>
    /// デフォルトのパネル、アクセサリーカスタマイズを非表示にして、カラーカスタマイズパネルを表示する
    /// </summary>
    public void ChangeToColorCustomizePanel()
    {
        BaseControlPanel.SetActive(false);
        ColorControlPanel.SetActive(true);
        ItemControlPanel.SetActive(false);
    }
}
