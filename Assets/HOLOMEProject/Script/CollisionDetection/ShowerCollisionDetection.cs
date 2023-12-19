using System.Collections;
using UnityEngine;

public class ShowerCollisionDetection : MonoBehaviour
{
    public GameObject showerObject;
    /// <summary>
    /// シャワーが使用できるかどうかを表すフラグ。
    /// </summary>
    public static bool isShowerUsed = true;
    private CharacterModel characterModel;

    public void SetCharacterModel(CharacterModel characterModel)
    {
        this.characterModel = characterModel;
    }

    /// <summary>
    /// 衝突時に発火
    /// </summary>
    /// <param name="collision">衝突情報</param>
    public void OnParticleCollision(GameObject collision)
    {
        StartCoroutine(HandleCollision(collision));
    }
    /// <summary>
    /// FIXME:bool型isShowerUsedがHandleCollision入るとfalseになる(Logで確認済み)、if外して強制trueで成長確認済み
    /// </summary>
    /// <param name="collision"></param>
    /// <returns></returns>
    private IEnumerator HandleCollision(GameObject collision)
    {
        bool isCollisionAndshowerUsed = isShowerUsed;
        if (isCollisionAndshowerUsed)
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
