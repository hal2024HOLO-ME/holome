using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �I�u�W�F�N�g�Ƃ̏Փˏ������s���B
/// </summary>
public class CollisionDetection : MonoBehaviour
{
    private CharacterModel characterModel;

    public void SetCharacterModel(CharacterModel characterModel)
    {
        this.characterModel = characterModel;
    }

    /// <summary>
    ///  �Q�[���I�u�W�F�N�g���m���ڐG�����^�C�~���O�Ŏ��s
    /// </summary>
    public void OnCollisionEnter(Collision collision)
    {
        // �Փ˂����I�u�W�F�N�g�̖��O���擾
        string collisionObjectName = collision.gameObject.name;
        HandleCollision(collisionObjectName);
    }

    /// <summary>
    /// �Փ˂����I�u�W�F�N�g�̖��O�����ɃA�j���[�V���������s����
    /// </summary>
    private void HandleCollision(string collisionObjectName)
    {
        // HealthMonitor�̎��Ԉȓ��̏ꍇ�́A�Փ˂��Ă��A�j���[�V�����𔭉΂����Ȃ��B
        HealthMonitor healthMonitor = characterModel.GetGameObject().GetComponent<HealthMonitor>();
        if (healthMonitor.CheckSleepTime()) return;

        // animator(bool)�����s����B
        Animator animator = characterModel.GetGameObject().GetComponent<Animator>();
        animator.SetTrigger("isHappy");

        // TODO�F�ڐG�����I�u�W�F�N�g��animationParameter�̕R�t�����쐬����B
        // �R�t����animationParameter�����s����B
    }
}
