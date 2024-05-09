using System.Linq;
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
        

        float noiseValue = 0; /*(noise.Evaluate(point * settings.Roughness + settings.Centre) + 1) * .5f;*/
        float frequency = 1;
        float amplitude = 1;

        if (settings.NumLayers >= 1)
        {
            frequency = settings.layerSettings[0].Roughness;

            for (int i = 0; i < settings.NumLayers; i++)
            {
                float v = noise.Evaluate(point * frequency + settings.Centre);
                noiseValue += (v + 1) * .5f * amplitude;
                frequency *= settings.layerSettings[i].Roughness;
                amplitude *= settings.Persistence;
            }
        }

        return noiseValue * settings.Strength;
    }
}
