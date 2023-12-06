using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeToItemPanel : MonoBehaviour
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
    /// �x�[�X�p�l���ƃJ���[�J�X�^�}�C�Y�p�l���̔�\��
    /// </summary>
    public void ChangeToItemCustomizePanel()
    {
        BaseControlPanel.SetActive(false);
        ColorControlPanel.SetActive(false);
        ItemControlPanel.SetActive(true);
    }
}
