using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeToColorPanel : MonoBehaviour
{
    public GameObject ColorControlPanel;
    public GameObject BaseControlPanel;
    public GameObject ItemControlPanel;

    /// <summary>
    /// �f�t�H���g�̃p�l���A�A�N�Z�T���[�J�X�^�}�C�Y���\���ɂ��āA�J���[�J�X�^�}�C�Y�p�l����\������
    /// </summary>
    public void ChangeToColorCustomizePanel()
    {
        BaseControlPanel.SetActive(false);
        ColorControlPanel.SetActive(true);
        ItemControlPanel.SetActive(false);
    }
}
