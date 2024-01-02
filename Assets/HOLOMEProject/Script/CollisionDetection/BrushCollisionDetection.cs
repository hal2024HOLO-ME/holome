using Const;
using System.Collections;
using UnityEngine;

public class BrushCollisionDetection : MonoBehaviour
{
    public GameObject brushObject;
    /// <summary>
    /// ブラシが使用できるかどうかを表すフラグ。
    /// </summary>
    public static bool isBrushUsed = true;
    private CharacterModel characterModel;

    private void Start()
    {
        // brushObjectをキャラクターの方に向ける
        brushObject.transform.LookAt(characterModel.GetGameObject().transform);
    }

    public void SetCharacterModel(CharacterModel characterModel)
    {
        this.characterModel = characterModel;
    }

    /// <summary>
    /// 衝突時に発火
    /// </summary>
    /// <param name="collision">衝突情報</param>
    public void OnCollisionEnter(Collision collision)
    {
        if( collision.gameObject.name == characterModel.GetGameObject().name )
        {
            Debug.Log("ブラシが衝突しました。");
            StartCoroutine(HandleCollision());
        }
    }

    /// <summary>
    /// 衝突時の処理をハンドリングする。
    /// </summary>
    /// <param name="collision">衝突情報</param>
    private IEnumerator HandleCollision()
    {
        ;
        if (isBrushUsed || characterModel.GetIsDead()) {
            /**
                1. charactermodelのアニメーションを再生する。
                2. 懐き度を上げる。
                3. ブラシは一回使用したら５分間は使用できないようにする。isBrushUsedをfalseにする。
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