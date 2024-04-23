using UnityEngine;

[ExecuteInEditMode()]
public class PlanetaryGeneration : MonoBehaviour
{
    public int PlanetSeed;

    public float PlanetRadius;
    public float PlanetMass;

    [Range(2, 256)]
    public int Resolution;

    CubeSphere cubeSphere;

    private void Awake()
    {
        cubeSphere = new CubeSphere();
    }

    public void Generate()
    {
        cubeSphere.Init(Resolution, gameObject);
    }
}
