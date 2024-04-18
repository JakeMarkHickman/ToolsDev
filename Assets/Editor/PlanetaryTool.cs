using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[CustomEditor(typeof(PlanetaryGeneration))]
public class PlanetaryTool : Editor
{
    SerializedProperty Resolution;

    
}


public class PlanetaryWindow : EditorWindow
{
    [MenuItem("Examples/Window")]
    public static void ShowExample()
    {
        PlanetaryWindow Window = GetWindow<PlanetaryWindow>();
        Window.titleContent = new GUIContent("MyEditorWindow");
    }

    public void CreateGUI()
    {
        VisualElement root = rootVisualElement;

        Label label = new Label("Planetary Generation Tool");
        root.Add(label);

        Button GenerateButton = new Button();
        GenerateButton.name = "Generate";
        GenerateButton.text = "GENERATE!";
        root.Add(GenerateButton);
    }
}
