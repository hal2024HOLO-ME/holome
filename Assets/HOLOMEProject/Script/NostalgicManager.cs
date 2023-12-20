using UnityEngine;
using UniRx;
using System;

using Const;

public class NostalgicManager : MonoBehaviour
{
    private CharacterModel characterModel;
    /// <summary>
    /// �������������s����^�C�}�[
    /// </summary>
    private IDisposable minuteTimeTrigger;

    private void Awake()
    {
        characterModel = gameObject.GetComponent<CharacterModel>();
        HealthMonitor healthMonitor = gameObject.GetComponent<HealthMonitor>();

        // ���ݎ�������10�����Ƃɏ��������s
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
                // �����x��1�̒i�K�ɂȂ�����A���S������B
                if (GetNostalgicStage() == 1)
                {
                    KillCharacterModel();
                }
            })
            .AddTo(this);
    }

    /// <summary>
    /// �����x�𑝉�������B
    /// </summary>
    public void IncreaseNostalgicLevel()
    {
        characterModel.SetNostalgicLevel(characterModel.GetNostalgicLevel() + 1);
    }

    /// <summary>
    /// �����x������������B
    /// </summary>
    public void DecreaseNostalgicLevel()
    {
        characterModel.SetNostalgicLevel(characterModel.GetNostalgicLevel() - 1);
    }

    /// <summary>
    /// �����x��i�K�ʂɂ��ԋp����B
    /// </summary>
    /// <returns>�����x�̒i�K��Ԃ��F1~5</returns>
    public int GetNostalgicStage()
    {
        int nostalgicLevel = characterModel.GetNostalgicLevel();

        if (nostalgicLevel < CO.MIN_NOSTALGIC_LEVEL) return 1;

        if (nostalgicLevel > CO.MAX_NOSTALGIC_LEVEL) return 5;

        return Mathf.CeilToInt(nostalgicLevel / 20.0f);
    }

    /// <summary>
    /// �����x�ɂ���āAobject�̃T�C�Y��ύX����B
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
        // �X���[�Y�ɃT�C�Y��ύX����B
        characterModel.GetGameObject().transform.localScale = 
            Vector3.Lerp(characterModel.GetGameObject().transform.localScale, vector3, 1f);
    }

    ///<summary>
    /// �����x�̒i�K���P�̒i�K��characterModel�����S������B�A�j���[�V�����Ȃǂ����΂����Ȃ��悤�ɂ���B
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
    /// scene���j�����ꂽ��A�^�C�}�[��j������B
    /// </summary>
    private void OnDestroy() => minuteTimeTrigger?.Dispose();
}
