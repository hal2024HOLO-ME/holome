using UnityEngine;
using UniRx;
using System;

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
                if (healthMonitor.CheckSleepTime()) return;
                int nostalgicStage = GetNostalgicStage();
                DecreaseNostalgicLevel();
                if (nostalgicStage != GetNostalgicStage())
                    ChangeObjectSize();
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

        if (nostalgicLevel < CharacterModel.MIN_NOSTALGIC_LEVEL) return 1;

        if (nostalgicLevel > CharacterModel.MAX_NOSTALGIC_LEVEL) return 5;

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
                vector3 = new(0.1f, 0.1f, 0.1f);
                break;
            case 2:
                vector3 = new(0.5f, 0.5f, 0.5f);
                break;
            case 3:
                vector3 = new(1f, 1f, 1f);
                break;
            case 4:
                vector3 = new(1.5f, 1.5f, 1.5f);
                break;
            case 5:
                vector3 = new(2f, 2f, 2f);
                break;
        }
        // �X���[�Y�ɃT�C�Y��ύX����B
        characterModel.GetGameObject().transform.localScale = 
            Vector3.Lerp(characterModel.GetGameObject().transform.localScale, vector3, 1f);
    }

    /// <summary>
    /// scene���j�����ꂽ��A�^�C�}�[��j������B
    /// </summary>
    private void OnDestroy() => minuteTimeTrigger?.Dispose();
}
