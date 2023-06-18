using UnityEditor;
using UnityEngine;

public abstract class ItemSalableEditor : ItemEditor
{
    protected ItemSalable _itemSalableTarget;
    protected override void OnEnable()
    {
        base.OnEnable();
        _itemSalableTarget = (ItemSalable)target;
    }

    public override void OnInspectorGUI()
    {
        EditorGUI.BeginChangeCheck();
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField(_itemTarget.name);
        SetAndDrawImage();
        EditorGUILayout.EndHorizontal();

        Description();
        ShowValue(); 
        ShowWeight();
        ShowDurability();
        
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(_itemTarget, "Modify Item");
            EditorUtility.SetDirty(_itemTarget);
        }
    }

    void ShowValue()
    {
        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        EditorGUILayout.LabelField("Value: ", GUILayout.ExpandWidth(false), GUILayout.Width(40));
        _itemSalableTarget.Value = EditorGUILayout.FloatField(_itemSalableTarget.Value, GUILayout.MinWidth(20), GUILayout.MaxWidth(40));
        EditorGUILayout.LabelField("$", GUILayout.Width(15));
        EditorGUILayout.EndHorizontal();
    }
}
