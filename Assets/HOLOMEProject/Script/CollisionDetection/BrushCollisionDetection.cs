using Const;
using System.Collections;
using UnityEngine;

public class BrushCollisionDetection : MonoBehaviour
{
    public GameObject brushObject;
    /// <summary>
    /// �u���V���g�p�ł��邩�ǂ�����\���t���O�B
    /// </summary>
    public static bool isBrushUsed = true;
    private CharacterModel characterModel;

    private void Start()
    {
        // brushObject���L�����N�^�[�̕��Ɍ�����
        brushObject.transform.LookAt(characterModel.GetGameObject().transform);
    }

    public void SetCharacterModel(CharacterModel characterModel)
    {
        this.characterModel = characterModel;
    }

    /// <summary>
    /// �Փˎ��ɔ���
    /// </summary>
    /// <param name="collision">�Փˏ��</param>
    public void OnCollisionEnter(Collision collision)
    {
        if( collision.gameObject.name == characterModel.GetGameObject().name )
        {
            Debug.Log("�u���V���Փ˂��܂����B");
            StartCoroutine(HandleCollision());
        }
    }

    /// <summary>
    /// �Փˎ��̏������n���h�����O����B
    /// </summary>
    /// <param name="collision">�Փˏ��</param>
    private IEnumerator HandleCollision()
    {
        ;
        if (isBrushUsed || characterModel.GetIsDead()) {
            /**
                1. charactermodel�̃A�j���[�V�������Đ�����B
                2. �����x���グ��B
                3. �u���V�͈��g�p������T���Ԃ͎g�p�ł��Ȃ��悤�ɂ���BisBrushUsed��false�ɂ���B
             */
            Animator animator = characterModel.GetGameObject().GetComponent<Animator>();
            animator.SetTrigger(CO.ANIMATOR_TRIGGER_HAPPY);

            NostalgicManager nostalgicManager = characterModel.GetGameObject().GetComponent<NostalgicManager>();
            int nostalgicStage = nostalgicManager.GetNostalgicStage();
            nostalgicManager.IncreaseNostalgicLevel();
            if (nostalgicStage != nostalgicManager.GetNostalgicStage())
            {
                nostalgicManager.ChangeObjectSize();
            }

            isBrushUsed = false;
        }
        yield return null;
    }
}