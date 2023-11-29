using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// オブジェクトとの衝突処理を行う。
/// </summary>
public class CollisionDetection : MonoBehaviour
{
    private CharacterModel characterModel;

    public void SetCharacterModel(CharacterModel characterModel)
    {
        this.characterModel = characterModel;
    }

    /// <summary>
    ///  ゲームオブジェクト同士が接触したタイミングで実行
    /// </summary>
    public void OnCollisionEnter(Collision collision)
    {
        // 衝突したオブジェクトの名前を取得
        string collisionObjectName = collision.gameObject.name;
        HandleCollision(collisionObjectName);
    }

    /// <summary>
    /// 衝突したオブジェクトの名前を元にアニメーションを実行する
    /// </summary>
    private void HandleCollision(string collisionObjectName)
    {
        // HealthMonitorの時間以内の場合は、衝突してもアニメーションを発火させない。
        HealthMonitor healthMonitor = characterModel.GetGameObject().GetComponent<HealthMonitor>();
        if (healthMonitor.CheckSleepTime()) return;

        // animator(bool)を実行する。
        Animator animator = characterModel.GetGameObject().GetComponent<Animator>();
        animator.SetTrigger("isHappy");

        // TODO：接触したオブジェクトとanimationParameterの紐付けを作成する。
        // 紐付けたanimationParameterを実行する。
    }
}
