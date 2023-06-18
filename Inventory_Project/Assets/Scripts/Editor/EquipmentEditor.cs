using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Equipment))]
public class EquipmentEditor : ItemSalableEditor
{
    Equipment _equipmentTarget;

    protected override void OnEnable()
    {
        base.OnEnable();
        _equipmentTarget = (Equipment)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUI.BeginChangeCheck();
        ShowType();
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(_itemTarget, "Type changed");
            EditorUtility.SetDirty(_itemTarget);
        }
    }

    void ShowType()
    {

        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        EditorGUILayout.LabelField("Type: ", GUILayout.ExpandWidth(false), GUILayout.Width(40));
        _equipmentTarget.typeOfEquipable = (Equipment.TypeOfEquipable)EditorGUILayout.EnumPopup(_equipmentTarget.typeOfEquipable, GUILayout.MinWidth(70), GUILayout.MaxWidth(90));
        EditorGUILayout.EndHorizontal();
    }
}