using System;
using UnityEngine;

using Const;

public class CharacterModel : MonoBehaviour
{
    private new GameObject gameObject;
    /// <summary>
    /// 懐き度を保持するフィールド
    /// </summary>
    private int nostalgicLevel = 50;
    /// <summary>
    /// 死亡判定Flg
    /// </summary>
    private bool isDead = false;


    public CharacterModel() { }

    public CharacterModel(GameObject gameObject)
    {
        this.gameObject = gameObject;
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public void SetGameObject(GameObject myGameObject)
    {
        this.gameObject = myGameObject;
    }

    public int GetNostalgicLevel()
    {
        return nostalgicLevel;
    }

    public void SetNostalgicLevel(int nostalgicLevel)
    {
        this.nostalgicLevel = Math.Clamp(nostalgicLevel, CO.MIN_NOSTALGIC_LEVEL, CO.MAX_NOSTALGIC_LEVEL);
    }

    public bool GetIsDead()
    {
        return isDead;
    }

    public void SetIsDead(bool isDead)
    {
        this.isDead = isDead;
    }
}
