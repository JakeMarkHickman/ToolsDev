using UnityEngine;

public class NoiseFilter
{
    NoiseSettings settings;
    Noise noise = new Noise();

    public NoiseFilter(NoiseSettings settings)
    {
        this.settings = settings;
    }

    public float Evaluate(Vector3 point)
    {
        float noiseValue = (noise.Evaluate(point * settings.Roughness + settings.Centre) + 1) * .5f;
        return noiseValue * settings.Strength;
    }
}
