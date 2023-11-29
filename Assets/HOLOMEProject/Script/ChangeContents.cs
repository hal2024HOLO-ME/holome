using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeContents : MonoBehaviour
{
    // �Z��I�u�W�F�N�g�̃��X�g
    private List<GameObject> siblingObjects = new List<GameObject>();

    // ���ݕ\�����Ă���Z��I�u�W�F�N�g�̃C���f�b�N�X
    private int currentSiblingIndex = 0;

    void Start()
    {
        // ���ׂĂ̌Z��I�u�W�F�N�g�����X�g�ɒǉ�
        for (int i = 2; i < transform.parent.childCount - 1; i++)
        {
            siblingObjects.Add(transform.parent.GetChild(i).gameObject);
        }

        // �ŏ��̌Z��I�u�W�F�N�g������\������
        if (siblingObjects.Count > 0)
        {
            siblingObjects[0].SetActive(true);
        }
    }

    // ���փ{�^���������ꂽ�Ƃ��ɌĂяo����郁�\�b�h
    public void OnNextButtonClicked()
    {
        // �����Ō�܂ŕ\�����Ă�����V�[����؂�ւ���
        if (currentSiblingIndex == siblingObjects.Count - 1)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("PersonalityDiagnosis");
            return;
        }

        // ���ݕ\�����Ă���Z��I�u�W�F�N�g���\���ɂ���
        siblingObjects[currentSiblingIndex].SetActive(false);

        // �C���f�b�N�X���X�V����
        currentSiblingIndex = (currentSiblingIndex + 1) % siblingObjects.Count;

        // �V�����Z��I�u�W�F�N�g��\������
        siblingObjects[currentSiblingIndex].SetActive(true);
    }
}
