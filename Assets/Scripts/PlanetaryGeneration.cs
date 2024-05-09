using UnityEngine;

[ExecuteInEditMode()]
public class PlanetaryGeneration : MonoBehaviour
{
    public int PlanetSeed;

    public PlanetSettings settings;

    [Range(2, 256)]
    public int Resolution;

    CubeSphere cubeSphere;

    private void Awake()
    {
        cubeSphere = new CubeSphere();
        settings = new PlanetSettings();
        settings.noiseSettings = new NoiseSettings();
        
    }

    public void Generate()
    {
        settings.noiseSettings.layerSettings = new LayerSettings[settings.noiseSettings.NumLayers];

        for (int i = 0; i < settings.noiseSettings.NumLayers; i++)
        {
            settings.noiseSettings.layerSettings[i] = new LayerSettings();
        }      
        cubeSphere.Init(Resolution, gameObject, settings);
    }
}
