using Codice.Client.Common.GameUI;
using System.Numerics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using System.Linq;
using UnityEngine.UIElements;

public class PlanetaryWindow : EditorWindow
{
    VisualElement root;
    VisualTreeAsset ToolTree;
    VisualElement LayersContainer;

    int PlanetRadius = 1;

    float Strength = 1.0f;
    float Persistence = 0.5f;
    int numLayers = 0;

    Slider[] NoiseLayers = new Slider[8];
    float[] Roughness;

    int Resolution = 2;

    

    [MenuItem("Tools/Planet Generation/Generation")]
    public static void ShowWindow()
    {
        PlanetaryWindow Window = GetWindow<PlanetaryWindow>();
        Window.titleContent = new GUIContent("Planet Generation Tool");
    }

    public void CreateGUI()
    {
        root = rootVisualElement;

        ToolTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/UXML/PlanetToolUI.uxml");
        ToolTree.CloneTree(root);

        //Planet Radius Int Field
        IntegerField PlanetRadiusIntField = root.Q<IntegerField>("PlanetRadiusIntField");
        //Planet Radius Int Field Event
        PlanetRadiusIntField.RegisterValueChangedCallback<int>(PlanetRadiusIntFieldChange);

        //Strength Slider
        Slider StrengthSlider = root.Q<Slider>("StrengthSlider");
        //Strenght Slider Event
        StrengthSlider.RegisterValueChangedCallback<float>(StrengthSliderChange);

        //Persistence Slider
        Slider PersistenceSlider = root.Q<Slider>("PersistenceSlider");
        //Persistence Slider Event
        PersistenceSlider.RegisterValueChangedCallback<float>(PersistanceSliderChange);

        //Layer Slider
        SliderInt NumLayersSlider = root.Q<SliderInt>("NumLayersSlider");
        //Layer Slider Event
        NumLayersSlider.RegisterValueChangedCallback<int>(NumLayersSliderChange);




        ////Resolution Slider
        SliderInt ResolutionSlider = root.Q<SliderInt>("ResolutionSlider");
        //Resolution Slider Event
        ResolutionSlider.RegisterValueChangedCallback<int>(ResolutionSiderChange);

        ////Generate Button
        Button GenerateButton = root.Q<Button>("GenerateButton");
        //Generate Button on click Event
        GenerateButton.RegisterCallback<ClickEvent>(GenerateClick);
    }

    private void PlanetRadiusIntFieldChange(ChangeEvent<int> value)
    {
        PlanetRadius = value.newValue;
    }

    private void StrengthSliderChange(ChangeEvent<float> value)
    {
        Strength = value.newValue;
    }

    private void PersistanceSliderChange(ChangeEvent<float> value)
    {
        Persistence = value.newValue;
    }

    private void NumLayersSliderChange(ChangeEvent<int> value)
    {
        int preLayers = numLayers;
        numLayers = value.newValue;
        

        LayersContainer = root.Q<VisualElement>("LayersContainer");

        VisualTreeAsset NoiseSetting = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/UXML/NoiseLayerSettings.uxml");

        

        if (preLayers < numLayers)
        {
            for (int i = preLayers; i < numLayers; i++)
            {
                NoiseLayers[i] = new Slider();
                NoiseLayers[i].name = "NoiseSlider" + i.ToString();

                NoiseLayers[i].highValue = 10;
                NoiseLayers[i].label = "Noise for Layer: " + i.ToString();

                LayersContainer.Add(NoiseLayers[i]);

                Roughness = new float[value.newValue];

                NoiseLayers[i].RegisterCallback<ChangeEvent<float>, int>(RoughnessSliderChange, i);
            }
        }
        else if (preLayers > numLayers)
        {
            for (int i = preLayers; i > numLayers; i--)
            {

                Roughness = new float[value.newValue];

                int calcIdx = i - 1;
                LayersContainer.RemoveAt(calcIdx);
                
            }
        }
    }

    private void RoughnessSliderChange(ChangeEvent<float> value, int index)
    {
        Roughness[index] = value.newValue;
    }


    private void ResolutionSiderChange(ChangeEvent<int> value)
    {
        Resolution = value.newValue;
    }

    private void GenerateClick(ClickEvent EventData)
    {
        GameObject SelectedGameObj = Selection.activeGameObject;

        if (SelectedGameObj != null && SelectedGameObj.GetComponent<PlanetaryGeneration>() != null)
        {
            Debug.Log("GameObject Selected");
            Generation(SelectedGameObj);
        }
        else
        {
            GameObject planet = new GameObject("Planet");
            planet.AddComponent<PlanetaryGeneration>();
            Generation(planet);
        }
    }

    private void Generation(GameObject PlanetObject)
    {
        PlanetObject.GetComponent<PlanetaryGeneration>().settings.PlanetRadius = PlanetRadius;
        PlanetObject.GetComponent<PlanetaryGeneration>().settings.noiseSettings.Strength = Strength;
        PlanetObject.GetComponent<PlanetaryGeneration>().settings.noiseSettings.NumLayers = numLayers;

        PlanetObject.GetComponent<PlanetaryGeneration>().settings.noiseSettings.layerSettings = new LayerSettings[numLayers];

        for (int i = 0; i < numLayers; i++)
        {
            PlanetObject.GetComponent<PlanetaryGeneration>().settings.noiseSettings.layerSettings[i] = new LayerSettings();
            Debug.Log(PlanetObject.GetComponent<PlanetaryGeneration>().settings.noiseSettings.layerSettings[i]);
            PlanetObject.GetComponent<PlanetaryGeneration>().settings.noiseSettings.layerSettings[i].Roughness = Roughness[i];
        }
        
        PlanetObject.GetComponent<PlanetaryGeneration>().settings.Resolution = Resolution;
        PlanetObject.GetComponent<PlanetaryGeneration>().Generate();
    }
}
