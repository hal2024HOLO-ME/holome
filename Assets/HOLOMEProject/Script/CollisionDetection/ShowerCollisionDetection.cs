using System.Collections;
using UnityEngine;

public class ShowerCollisionDetection : MonoBehaviour
{
    public GameObject showerObject;
    /// <summary>
    /// �V�����[���g�p�ł��邩�ǂ�����\���t���O�B
    /// </summary>
    public static bool isShowerUsed = true;
    public bool isCollisionAndshowerUsed = isShowerUsed;
    private CharacterModel characterModel;

    public void SetCharacterModel(CharacterModel characterModel)
    {
        this.characterModel = characterModel;
    }

    /// <summary>
    /// �Փˎ��ɔ���
    /// </summary>
    /// <param name="collision">�Փˏ��</param>
    public void OnParticleCollision(GameObject collision)
    {
        StartCoroutine(HandleCollision(collision));
    }
    /// <param name="collision"></param>
    /// <returns></returns>
    private IEnumerator HandleCollision(GameObject collision)
    {
        isShowerUsed = true;
        Debug.Log(isCollisionAndshowerUsed);
        if (isCollisionAndshowerUsed || characterModel.GetIsDead())
        {
            Animator animator = characterModel.GetGameObject().GetComponent<Animator>();
            animator.SetTrigger("HappyTrigger");
            NostalgicManager nostalgicManager = characterModel.GetGameObject().GetComponent<NostalgicManager>();
            int nostalgicStage = nostalgicManager.GetNostalgicStage();
            nostalgicManager.IncreaseNostalgicLevel();
            if (nostalgicStage != nostalgicManager.GetNostalgicStage())
            {
                nostalgicManager.ChangeObjectSize();
            }
             isShowerUsed = false;
        }
        yield return null;
    }


}
