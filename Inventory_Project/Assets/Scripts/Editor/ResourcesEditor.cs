using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Resources))]
public class ResourcesEditor : ItemEditor
{
    Resources _resourcesTarget;

    protected override void OnEnable()
    {
        base.OnEnable();
        _resourcesTarget = (Resources)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        ShowValue();
    }

    void ShowValue()
    {
        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        EditorGUILayout.LabelField("Value: ", GUILayout.ExpandWidth(false), GUILayout.Width(40));
        _resourcesTarget.Value = EditorGUILayout.FloatField(_resourcesTarget.Value, GUILayout.MinWidth(20), GUILayout.MaxWidth(40));
        EditorGUILayout.LabelField("$", GUILayout.Width(15));
        EditorGUILayout.EndHorizontal();
    }
}
