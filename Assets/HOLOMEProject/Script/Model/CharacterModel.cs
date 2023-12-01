using System;
using UnityEngine;

public class CharacterModel : MonoBehaviour
{
    private new GameObject gameObject;
    /// <summary>
    /// 懐き度を保持するフィールド
    /// </summary>
    private int nostalgicLevel = 50;
    /// <summary>
    /// 懐き度の最大値
    /// </summary>
    public const int MAX_NOSTALGIC_LEVEL = 100;
    /// <summary>
    /// 懐き度の最小値
    /// </summary>
    public const int MIN_NOSTALGIC_LEVEL = 0;

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
        this.nostalgicLevel = Math.Clamp(nostalgicLevel, MIN_NOSTALGIC_LEVEL, MAX_NOSTALGIC_LEVEL);
    }
}
