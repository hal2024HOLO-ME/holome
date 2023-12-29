using UnityEngine;

[System.Serializable]
public class CharacterData
{
    public ColorData color;
    public CustomizeData customize;

    [System.Serializable]
    public class ColorData
    {
        public ColorComponents eye;
        public ColorComponents ear;
        public ColorComponents body;
    }

    [System.Serializable]
    public class CustomizeData
    {
        public string neck;
        public string head;
        public string face;
    }

    [System.Serializable]
    public class ColorComponents
    {
        public float r;
        public float g;
        public float b;
        public float a;
    }
}
