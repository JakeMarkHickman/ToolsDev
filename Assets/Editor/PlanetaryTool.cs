using System.Numerics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class PlanetaryWindow : EditorWindow
{
    float Strength = 1.0f;
    int Resolution = 2;

    [MenuItem("Tools/Planet Generation/Generation")]
    public static void ShowWindow()
    {
        PlanetaryWindow Window = GetWindow<PlanetaryWindow>();
        Window.titleContent = new GUIContent("Planet Generation Tool");
    }

    public void CreateGUI()
    {
        VisualElement root = rootVisualElement;

        VisualTreeAsset ToolTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/UXML/PlanetToolUI.uxml");
        ToolTree.CloneTree(root);

        //Strength Slider
        Slider StrengthSlider = root.Q<Slider>("StrengthSlider");
        //Strenght Slider Event
        StrengthSlider.RegisterValueChangedCallback<float>(StrengthSliderChange);

        ////Resolution Slider
        SliderInt ResolutionSlider = root.Q<SliderInt>("ResolutionSlider");
        //Resolution Slider Event
        ResolutionSlider.RegisterValueChangedCallback<int>(ResolutionSiderChange);

        ////Generate Button
        Button GenerateButton = root.Q<Button>("GenerateButton");
        //Generate Button on click Event
        GenerateButton.RegisterCallback<ClickEvent>(GenerateClick);
    }

    private void StrengthSliderChange(ChangeEvent<float> value)
    {
        Strength = value.newValue;
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
        PlanetObject.GetComponent<PlanetaryGeneration>().Resolution = Resolution;
        PlanetObject.GetComponent<PlanetaryGeneration>().settings.noiseSettings.Strength = Strength;
        PlanetObject.GetComponent<PlanetaryGeneration>().Generate();
    }
}
