using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterModel : MonoBehaviour
{
    private new GameObject gameObject;
    private AnimatorControllerParameter[] animatorParameters;
    /// <summary>
    /// 懐き度を保持するフィールド
    /// </summary>
    private int nostalgicLevel = 0;

    public CharacterModel() { }

    public CharacterModel(GameObject gameObject, AnimatorControllerParameter[] animatorParameters)
    {
        this.gameObject = gameObject;
        this.animatorParameters = animatorParameters;
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public void SetGameObject(GameObject myGameObject)
    {
        this.gameObject = myGameObject;
    }

    public AnimatorControllerParameter[] GetAnimatorParameters()
    {
        return animatorParameters;
    }

    public void SetAnimatorParameters(AnimatorControllerParameter[] animatorParameters)
    {
        this.animatorParameters = animatorParameters;
    }

    public int GetNostalgicLevel()
    {
        return nostalgicLevel;
    }

    public void SetNostalgicLevel(int nostalgicLevel)
    {
        this.nostalgicLevel = nostalgicLevel;
    }
}
