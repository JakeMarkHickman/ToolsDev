using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class PlanetSettings : ScriptableObject
{
    public float PlanetRadius = 4;
    public NoiseSettings noiseSettings;
    public bool DynamicPlanet = false;
    public int Resolution;
}
