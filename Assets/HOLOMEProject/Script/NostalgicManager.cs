using UnityEngine;
using UniRx;
using System;

using Const;

public class NostalgicManager : MonoBehaviour
{
    private CharacterModel characterModel;
    /// <summary>
    /// 減少処理を実行するタイマー
    /// </summary>
    private IDisposable minuteTimeTrigger;

    private void Awake()
    {
        characterModel = gameObject.GetComponent<CharacterModel>();
        HealthMonitor healthMonitor = gameObject.GetComponent<HealthMonitor>();

        // 現在時刻から10分ごとに処理を実行
        minuteTimeTrigger = Observable
            .Timer(TimeSpan.FromMinutes(10.0f - DateTime.Now.Minute % 10), TimeSpan.FromMinutes(10.0f))
            .SubscribeOnMainThread()
            .Subscribe(x =>
            {
                if (healthMonitor.CheckSleepTime() || characterModel.GetIsDead()) return;
                int nostalgicStage = GetNostalgicStage();
                DecreaseNostalgicLevel();
                if (nostalgicStage != GetNostalgicStage())
                {
                    ChangeObjectSize();
                }
                // 懐き度が1の段階になったら、死亡させる。
                if (GetNostalgicStage() == 1)
                {
                    KillCharacterModel();
                }
            })
            .AddTo(this);
    }

    /// <summary>
    /// 懐き度を増加させる。
    /// </summary>
    public void IncreaseNostalgicLevel()
    {
        characterModel.SetNostalgicLevel(characterModel.GetNostalgicLevel() + 1);
    }

    /// <summary>
    /// 懐き度を減少させる。
    /// </summary>
    public void DecreaseNostalgicLevel()
    {
        characterModel.SetNostalgicLevel(characterModel.GetNostalgicLevel() - 1);
    }

    /// <summary>
    /// 懐き度を段階別にし返却する。
    /// </summary>
    /// <returns>懐き度の段階を返す：1~5</returns>
    public int GetNostalgicStage()
    {
        int nostalgicLevel = characterModel.GetNostalgicLevel();

        if (nostalgicLevel < CO.MIN_NOSTALGIC_LEVEL) return 1;

        if (nostalgicLevel > CO.MAX_NOSTALGIC_LEVEL) return 5;

        return Mathf.CeilToInt(nostalgicLevel / 20.0f);
    }

    /// <summary>
    /// 懐き度によって、objectのサイズを変更する。
    /// </summary>
    public void ChangeObjectSize()
    {
        int nostalgicStage = GetNostalgicStage();
        Vector3 vector3 = new();
        switch (nostalgicStage)
        {
            case 1:
                vector3 = new(0.025f, 0.025f, 0.025f);
                break;
            case 2:
                vector3 = new(0.05f, 0.05f, 0.05f);
                break;
            case 3:
                vector3 = new(0.1f, 0.1f, 0.1f);
                break;
            case 4:
                vector3 = new(0.2f, 0.2f, 0.2f);
                break;
            case 5:
                vector3 = new(0.3f, 0.3f, 0.3f);
                break;
        }
        // スムーズにサイズを変更する。
        characterModel.GetGameObject().transform.localScale = 
            Vector3.Lerp(characterModel.GetGameObject().transform.localScale, vector3, 1f);
    }

    ///<summary>
    /// 懐き度の段階が１の段階でcharacterModelを死亡させる。アニメーションなども発火させないようにする。
    /// </summary>
    public void KillCharacterModel()
    {
        GameObject gameObject = characterModel.GetGameObject();

        GameObject angelRingObject = gameObject.transform.Find("angelRing").gameObject;
        angelRingObject.SetActive(true);

        Animator animator = gameObject.GetComponent<Animator>();
        animator.SetBool(CO.ANIMATOR_BOOL_DEAD, true);

        characterModel.SetIsDead(true);
    }

    /// <summary>
    /// sceneが破棄されたら、タイマーを破棄する。
    /// </summary>
    private void OnDestroy() => minuteTimeTrigger?.Dispose();
}
