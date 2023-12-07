using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeToBasePanel : MonoBehaviour
{
    public GameObject ColorControlPanel;
    public GameObject BaseControlPanel;
    public GameObject ItemControlPanel;

    /// <summary>
    /// カラーカスタマイズパネル、アクセサリーカスタマイズを非表示にしてデフォルトのパネルを表示する
    /// </summary>
    void Start()
    {
        ColorControlPanel.SetActive(false);
        ItemControlPanel.SetActive(false);
    }

    /// <summary>
    /// カラーカスタマイズパネル、アクセサリーカスタマイズを非表示にしてデフォルトのパネルを表示する
    /// </summary>
    public void ChangeToBaseControlPanel()
    {
        BaseControlPanel.SetActive(true);
        ColorControlPanel.SetActive(false);
        ItemControlPanel.SetActive(false);
    }
}
