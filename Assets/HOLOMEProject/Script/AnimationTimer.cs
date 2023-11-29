using UnityEngine;
using UniRx;
using System;
using UniRx.Triggers;

public class AnimationTimer : MonoBehaviour
{
    private Animator animator;
    private HealthMonitor healthMonitor;
    private IDisposable timerAnimator = Disposable.Empty;
    // タイマを止めるためのフラグ
    private bool isTimePassed = false; // true: タイマーが止まっている状態

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

        // Idle状態になったらタイマーをセットし、カウントダウンをする。
        trigger
           .OnStateEnterAsObservable()
           .Where(x => x.StateInfo.IsName("Idle"))
           .Subscribe(_ => {
               if (!healthMonitor.CheckSleepTime() && !isTimePassed) TimerSet();
           }).AddTo(this);

        // Idle状態以外になったらタイマーを止める。
        trigger
            .OnStateEnterAsObservable()
            .Where(x => x.StateInfo.IsName("Idle") == false)
            .Subscribe(x =>
            {
                timerAnimator.Dispose();
            }).AddTo(this);
    }


    /// <summary>
    /// Timerを30秒に設定する。
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
    /// タイマー制約付きでアニメーションを発火させる。
    /// </summary>
    /// <param name="triggerName"></param>
    private void FireAnimatorTriggerWithTimer(string triggerName) => animator.SetTrigger(triggerName);
    

    public void OnDestroy() => timerAnimator.Dispose();
}
