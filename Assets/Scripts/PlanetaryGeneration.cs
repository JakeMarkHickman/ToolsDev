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
        cubeSphere.Init(Resolution, gameObject, settings);
    }
}
