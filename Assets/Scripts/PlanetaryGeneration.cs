using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[ExecuteInEditMode()]
public class PlanetaryGeneration : MonoBehaviour
{
    public int PlanetSeed;

    public PlanetSettings settings;

    [Range(2, 256)]
    public int Resolution;

    CubeSphere cubeSphere;

    public static Transform Camera; //Ref to the players camera

    //public static Dictionary<int, float> LOD = new Dictionary<int, float>()
    //{
    //    {0, Mathf.Infinity},
    //    {1, 60f},
    //    {2, 25f},
    //    {3, 10f},
    //    {4, 4f},
    //    { }
    //    { }
    //    { }

    //}

    private void Awake()
    {
        cubeSphere = new CubeSphere();
        settings = ScriptableObject.CreateInstance<PlanetSettings>();
        //settings = new PlanetSettings();
        settings.noiseSettings = new NoiseSettings();

    }

    public void Generate()
    {
        //settings.noiseSettings.layerSettings = new LayerSettings[settings.noiseSettings.NumLayers];

        //for (int i = 0; i < settings.noiseSettings.NumLayers; i++)
        //{
        //    settings.noiseSettings.layerSettings[i] = new LayerSettings();
        //}      
        cubeSphere.Init(Resolution, gameObject, settings);
    }
}
