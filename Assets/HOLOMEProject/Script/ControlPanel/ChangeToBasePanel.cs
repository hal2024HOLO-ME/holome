using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeToBasePanel : MonoBehaviour
{
    public GameObject ColorControlPanel;
    public GameObject BaseControlPanel;
    public GameObject ItemControlPanel;

    /// <summary>
    /// �J���[�J�X�^�}�C�Y�p�l���A�A�N�Z�T���[�J�X�^�}�C�Y���\���ɂ��ăf�t�H���g�̃p�l����\������
    /// </summary>
    void Start()
    {
        ColorControlPanel.SetActive(false);
        ItemControlPanel.SetActive(false);
    }

    /// <summary>
    /// �J���[�J�X�^�}�C�Y�p�l���A�A�N�Z�T���[�J�X�^�}�C�Y���\���ɂ��ăf�t�H���g�̃p�l����\������
    /// </summary>
    public void ChangeToBaseControlPanel()
    {
        BaseControlPanel.SetActive(true);
        ColorControlPanel.SetActive(false);
        ItemControlPanel.SetActive(false);
    }
}
