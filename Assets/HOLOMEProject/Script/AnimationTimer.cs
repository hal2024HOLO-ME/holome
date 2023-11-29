using UnityEngine;
using UniRx;
using System;
using UniRx.Triggers;

public class AnimationTimer : MonoBehaviour
{
    private Animator animator;
    private HealthMonitor healthMonitor;
    private IDisposable timerAnimator = Disposable.Empty;
    // �^�C�}���~�߂邽�߂̃t���O
    private bool isTimePassed = false; // true: �^�C�}�[���~�܂��Ă�����

    public void SetIsTimePassed(bool value)
    {
        isTimePassed = value;
    }

    private void Awake()
    {
        if (!gameObject.TryGetComponent(out animator)) return;
        if (!gameObject.TryGetComponent(out healthMonitor)) return;
    }

    void Start()
    {
        ObservableStateMachineTrigger trigger = animator.GetBehaviour<ObservableStateMachineTrigger>();

        // Idle��ԂɂȂ�����^�C�}�[���Z�b�g���A�J�E���g�_�E��������B
        trigger
           .OnStateEnterAsObservable()
           .Where(x => x.StateInfo.IsName("Idle"))
           .Subscribe(_ => {
               if (!healthMonitor.CheckSleepTime() && !isTimePassed) TimerSet();
           }).AddTo(this);

        // Idle��ԈȊO�ɂȂ�����^�C�}�[���~�߂�B
        trigger
            .OnStateEnterAsObservable()
            .Where(x => x.StateInfo.IsName("Idle") == false)
            .Subscribe(x =>
            {
                timerAnimator.Dispose();
            }).AddTo(this);
    }


    /// <summary>
    /// Timer��30�b�ɐݒ肷��B
    /// </summary>
    public void TimerSet()
    {
        timerAnimator.Dispose();

        timerAnimator = Observable
            .Timer(TimeSpan.FromSeconds(30))
            .Subscribe(_ => FireAnimatorTriggerWithTimer("WalkTrigger"))
            .AddTo(this);
    }

    /// <summary>
    /// �^�C�}�[����t���ŃA�j���[�V�����𔭉΂�����B
    /// </summary>
    /// <param name="triggerName"></param>
    private void FireAnimatorTriggerWithTimer(string triggerName) => animator.SetTrigger(triggerName);
    

    public void OnDestroy() => timerAnimator.Dispose();
}
