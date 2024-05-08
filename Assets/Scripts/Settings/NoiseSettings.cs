using UnityEngine;

[System.Serializable]
public class NoiseSettings
{
    public float Strength = 1;
    [Range(1, 8)]
    public int NumLayers = 1;

    public LayerSettings[] layerSettings;

    //public float BaseRoughness = 1;
    //public float Roughness = 1;
    public float Persistence = .5f;
    public Vector3 Centre;
}

public class LayerSettings
{
    public float Roughness = 1;
}
