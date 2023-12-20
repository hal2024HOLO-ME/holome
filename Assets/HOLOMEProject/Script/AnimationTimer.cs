using UnityEngine;
using UniRx;
using System;
using UniRx.Triggers;
using Const;

public class AnimationTimer : MonoBehaviour
{
    private CharacterModel characterModel;
    private HealthMonitor healthMonitor;
    private IDisposable timerAnimator = Disposable.Empty;
    private bool isTimePassed = false;

    public void SetIsTimePassed(bool value)
    {
        isTimePassed = value;
    }

    private void Awake()
    {
        if (!gameObject.TryGetComponent(out healthMonitor)) return;
    }

    public void SetCharacterModel(CharacterModel characterModel)
    {
        this.characterModel = characterModel;
    }

    void Start()
    {
        Animator animator = characterModel.GetGameObject().GetComponent<Animator>();
        ObservableStateMachineTrigger trigger = animator.GetBehaviour<ObservableStateMachineTrigger>();

        // Idle��ԂɂȂ�����^�C�}�[���Z�b�g���A�J�E���g�_�E��������B
        trigger
           .OnStateEnterAsObservable()
           .Where(x => x.StateInfo.IsName("Idle"))
           .Subscribe(_ => {
               bool shouldPerformAction = !healthMonitor.CheckSleepTime() || !characterModel.GetIsDead() && !isTimePassed;
               if (shouldPerformAction) TimerSet();
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
            .Subscribe(_ => FireAnimatorTriggerWithTimer())
            .AddTo(this);
    }

    /// <summary>
    /// �^�C�}�[����t���ŃA�j���[�V�����𔭉΂�����B
    /// </summary>
    /// <param name="triggerName"></param>
    private void FireAnimatorTriggerWithTimer()
    {
        Animator animator = characterModel.GetGameObject().GetComponent<Animator>();
        animator.SetTrigger(CO.ANIMATOR_TRIGGER_WALK);
    }
    

    public void OnDestroy() => timerAnimator.Dispose();
}