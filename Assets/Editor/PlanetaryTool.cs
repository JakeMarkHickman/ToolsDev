using System.Numerics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class PlanetaryWindow : EditorWindow
{
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

        Label ResolutionLable = new Label();
        ResolutionLable.name = "Resolution Lable";
        ResolutionLable.text = "Resolution";
        root.Add(ResolutionLable);



        //Resolution Slider
        SliderInt ResolutionSlider = new SliderInt(2, 256);
        ResolutionSlider.name = "Resolution";
        ResolutionSlider.tooltip = "Changes the resolution of the planet";
        root.Add(ResolutionSlider);

        //Resolution Slider Event
        ResolutionSlider.RegisterValueChangedCallback<int>(ResolutionSiderChange);



        //Generate Button
        Button GenerateButton = new Button();
        GenerateButton.name = "Generate";
        GenerateButton.text = "Generate";
        GenerateButton.tooltip = "If a game object is selected with a planet component in, it will edit the currently selected game object - else it will generate a new one";
        root.Add(GenerateButton);
        
        //Generate Button on click Event
        GenerateButton.RegisterCallback<ClickEvent>(GenerateClick);
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
        PlanetObject.GetComponent<PlanetaryGeneration>().Generate();
    }
}
