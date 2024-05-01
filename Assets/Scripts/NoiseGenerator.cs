using UnityEngine;

public class NoiseGenerator
{
    PlanetSettings settings;
    NoiseFilter noiseFilter;

    public NoiseGenerator(PlanetSettings settings)
    {
        this.settings = settings;
        noiseFilter = new NoiseFilter(settings.noiseSettings);
    }

    public Vector3 CalculatePointOnPlanet(Vector3 pointOnUnitSphere)
    {
        float elevation = noiseFilter.Evaluate(pointOnUnitSphere);
        return pointOnUnitSphere * settings.planetRadius * (1+elevation);
    }
}
