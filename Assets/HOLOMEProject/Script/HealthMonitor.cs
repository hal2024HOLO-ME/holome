using System;
using UnityEngine;
using UniRx;

using Const;

/// <summary>
/// �I�u�W�F�N�g�̏�Ԃ��Ď�����B
/// ����̎��ԑсi��F�������ԁj�ɑ΂��鏈�����s���B
/// </summary>
public class HealthMonitor : MonoBehaviour
{
    private CharacterModel characterModel;
    public DateTime dateTime = new DateTime();
    /// <summary>
    /// ��������(������)�̃g���K�[
    /// </summary>
    IDisposable minuteTimeTrigger;

    void Awake()
    {
        // CharacterModel���擾����
        characterModel = gameObject.GetComponent<CharacterModel>();

        // ���߂Ɉ����s���A�ȍ~��1�����Ɏ��s����B
        SetSleepStateIfInSleepTime();

        // 1�����Ƀg���K�[�����s����B
        minuteTimeTrigger = UniRx.Observable
            .Timer(TimeSpan.FromSeconds(60.0f - DateTime.Now.Second), TimeSpan.FromMinutes(1.0f))
            .SubscribeOnMainThread()
            .Subscribe(x =>
            {
                SetSleepStateIfInSleepTime();
            })
            .AddTo(this);
    }

    /// <summary>
    /// ���Ԕ͈͂Ȃ�A�Q��Ԃɂ���B
    /// </summary>
    private void SetSleepStateIfInSleepTime()
    {
        Animator animator = characterModel.GetGameObject().GetComponent<Animator>();

        bool isSleepTime = CheckSleepTime();

        bool isSleepAnimator = animator.GetBool(CO.ANIMATOR_BOOL_SLEEP);
        if( !isSleepTime )
        {
            if( isSleepAnimator )
            {
                animator.SetBool(CO.ANIMATOR_BOOL_SLEEP, false);
            }
            return;
        }

        // ���������͈̔͂̏ꍇ�́A������Ԃɂ���B
        animator.SetBool(CO.ANIMATOR_BOOL_SLEEP, true);
    }

    /// <summary>
    /// �����������`�F�b�N����
    /// </summary>
    /// <returns>���������͈̔͂Ȃ�true��Ԃ�</returns>
    public bool CheckSleepTime()
    {
        // ���݂̎������擾����
        TimeSpan timeOfDay = DateTime.Now.TimeOfDay;
        Debug.Log(timeOfDay);

        // 00:00 �` 07:00 �͈̔͂��`�F�b�N����
        TimeSpan startTime = new(0, 0, 0);
        TimeSpan endTime = new(7, 0, 0);

        // ���Ԃ͈͓̔���
        return ((startTime <= timeOfDay) && (timeOfDay <= endTime));
    }
}
