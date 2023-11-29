using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Utilities;
using UniRx;
using Unity.VisualScripting;

/// <summary>
/// オブジェクトの状態を監視する。
/// 特定の時間帯（例：睡眠時間）に対する処理を行う。
/// </summary>
public class HealthMonitor : MonoBehaviour
{
    private CharacterModel characterModel;
    public readonly DateTime dateTime;
    /// <summary>
    /// 時刻判定(分刻み)のトリガー
    /// </summary>
    IDisposable minuteTimeTrigger;

    public HealthMonitor()
    {
        dateTime = DateTime.Now;
    }

    public HealthMonitor(DateTime dateTime)
    {
        this.dateTime = dateTime;
    }

    void Awake()
    {
        // CharacterModelを取得する
        characterModel = gameObject.GetComponent<CharacterModel>();

        // 初めに一回実行し、以降は1分毎に実行する。
        SetSleepStateIfInSleepTime();

        // 1分毎にトリガーを実行する。
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
    /// 時間範囲なら就寝状態にする。
    /// </summary>
    private void SetSleepStateIfInSleepTime()
    {
        Animator animator = characterModel.GetGameObject().GetComponent<Animator>();

        bool isSleepTime = CheckSleepTime();

        bool isSleepAnimator = animator.GetBool("isSleep");
        if( !isSleepTime )
        {
            if( isSleepAnimator )
            {
                animator.SetBool("isSleep", false);
            }
            return;
        }

        // 睡眠時刻の範囲の場合は、睡眠状態にする。
        animator.SetBool("isSleep", true);
    }

    /// <summary>
    /// 睡眠時刻をチェックする
    /// </summary>
    /// <returns>睡眠時刻の範囲ならtrueを返す</returns>
    public bool CheckSleepTime()
    {
        // 現在の時刻を取得
        TimeSpan timeOfDay = dateTime.TimeOfDay;

        // 00:00 ～ 07:00 の範囲をチェックする
        TimeSpan startTime = new(0, 0, 0);
        TimeSpan endTime = new(7, 0, 0);

        // 時間の範囲内か
        return ((startTime <= timeOfDay) && (timeOfDay <= endTime));
    }
}
