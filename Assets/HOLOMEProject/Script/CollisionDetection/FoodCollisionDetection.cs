using System;
using System.Collections;
using UniRx;
using UnityEngine;

/// <summary>
/// 食べ物との衝突を検知し、アニメーションや移動などの処理を制御するクラス
/// </summary>
public class FoodCollisionDetection : MonoBehaviour
{
    private GameObject foodObject;
    private Animator foodAnimator;
    private CharacterModel characterModel;

    private void Awake()
    {
        foodObject = GameObject.Find("food");
        foodAnimator = foodObject.GetComponent<Animator>();
    }

    public void SetCharacterModel(CharacterModel characterModel)
    {
        this.characterModel = characterModel;
    }

    /// <summary>
    /// 衝突時に発火
    /// </summary>
    /// <param name="collision">衝突情報</param>
    void OnCollisionEnter(Collision collision)
    {
        StartCoroutine(HandleCollision(collision));
    }

    /// <summary>
    /// 衝突時の処理をハンドリングする。
    /// </summary>
    /// <param name="collision">衝突情報</param>
    private IEnumerator HandleCollision(Collision collision)
    {
        GameObject tableObject = GameObject.Find("Table");

        // animationTimerを停止する。
        AnimationTimer animationTimer = characterModel.GetGameObject().GetComponent<AnimationTimer>();
        animationTimer.SetIsTimePassed(true);

        bool isReadyToEat = foodAnimator.GetBool("isEat");
        if (collision.gameObject.name == tableObject.name
            && collision.contacts[0].normal == Vector3.up && !isReadyToEat)
        {
            // Characterの現在位置を保存する。
            // ご飯を食べ終わったらもとの場所に戻すため。
            GameObject characterObject = characterModel.GetGameObject();
            Animator characterAnimator = characterObject.GetComponent<Animator>();
            Vector3 characterPositionSave = characterObject.transform.position;

            Vector3 foodPosition = foodObject.transform.position;

            // 移動する方向に向く
            characterObject.transform.LookAt(foodPosition);

            /**
                TODO: 移動アニメーション追加。
                資料: https://gametukurikata.com/basic/animationmove
                資料：https://tsubakit1.hateblo.jp/entry/2015/03/07/233000
             */

            yield return StartCoroutine(MoveToDestination(characterObject, foodPosition).ToYieldInstruction());

            // 食べるアニメーションをトリガー
            yield return StartCoroutine(TriggerEatAnimation(characterAnimator));

            // 30秒待機
            yield return new WaitForSeconds(30f);

            // Characterを元の位置に戻す
            characterObject.transform.LookAt(characterPositionSave);
            yield return StartCoroutine(MoveToDestination(characterObject, characterPositionSave).ToYieldInstruction());
        }
        animationTimer.SetIsTimePassed(false);
        animationTimer.TimerSet();
    }

    /// <summary>
    /// 移動が完了したことを通知する
    /// </summary>
    /// <param name="characterObject">移動させる GameObject。</param>
    /// <param name="target">目標位置。</param>
    /// <returns>移動完了通知用の Observable。</returns>
    IObservable<Unit> MoveToDestination(GameObject characterObject, Vector3 target)
    {
        return Observable.FromCoroutine<Unit>((observer, cancellationToken) => {
            return MoveToDestinationCoroutine(characterObject, target, observer);
        });
    }

    /// <summary>
    /// 指定された GameObject を対象位置に移動
    /// </summary>
    /// <param name="characterObject">移動させる GameObject。</param>
    /// <param name="target">目標位置。</param>
    /// <param name="observer">移動完了通知用 Observer。</param>
    /// <returns>Coroutine実行に使用する IEnumerator。</returns>
    private IEnumerator MoveToDestinationCoroutine(
        GameObject characterObject,
        Vector3 target,
        IObserver<Unit> observer
    ){
        float duration = 2f;
        while (Vector3.Distance(characterObject.transform.position, target) > 0.7f)
        {
            characterObject.transform.position = 
                Vector3.Lerp(characterObject.transform.position, target, Time.deltaTime / duration);
            yield return null;
        }
        observer.OnNext(Unit.Default);
        observer.OnCompleted();
    }

    /// <summary>
    /// 食べるアニメーションを発火させる。
    /// </summary>
    /// <param name="animator"></param>
    private IEnumerator TriggerEatAnimation(Animator animator)
    {
        // 食べるアニメーションをトリガー
        animator.SetTrigger("EatTrigger");
        foodAnimator.SetBool("isEat", true);

        // アニメーションの終了を待つ
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
    }
}