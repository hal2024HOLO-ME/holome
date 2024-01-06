using System;
using UnityEngine;

[Serializable]
public class PartnerColor
{
    public Color eye;
    public Color ear;
    public Color body;
}

[Serializable]
public class Customize
{
    public string neck;
    public string head;
    public string face;
}

[Serializable]
public class CharacterData
{
    public int nostalgic_level;
    public PartnerColor color;
    public Customize customize;
}