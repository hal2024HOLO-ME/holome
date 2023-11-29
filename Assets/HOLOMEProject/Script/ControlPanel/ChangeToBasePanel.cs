using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeToBasePanel : MonoBehaviour
{
    public GameObject ColorControlPanel;
    public GameObject BaseControlPanel;

    /// <summary>
    /// カラーカスタマイズパネルを非表示にしてデフォルトのパネルを表示する
    /// </summary>
    public void ChangeToBaseControlPanel()
    {
        BaseControlPanel.SetActive(true);
        ColorControlPanel.SetActive(false);
    }
}
