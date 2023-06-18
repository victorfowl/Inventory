using UnityEditor;
using UnityEngine;

public abstract class ItemEditor : Editor
{
    protected Item _itemTarget;
    protected virtual void OnEnable()
    {
        _itemTarget = (Item)target;
    }

    public override void OnInspectorGUI()
    {
        EditorGUI.BeginChangeCheck();
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField(_itemTarget.name);
        SetAndDrawImage();
        EditorGUILayout.EndHorizontal();

        Description();
        ShowWeight();
        ShowDurability();

        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(_itemTarget, "Modify Item");
            EditorUtility.SetDirty(_itemTarget);
        }
    }

    protected void SetAndDrawImage()
    {
        EditorGUILayout.BeginVertical();
        EditorGUILayout.LabelField("Item Image");
        if (_itemTarget.ItemImage)
        {
            Texture2D texture = AssetPreview.GetAssetPreview(_itemTarget.ItemImage);
            GUILayout.Label("", GUILayout.Height(80), GUILayout.Width(80));
            if(texture)
            GUI.DrawTexture(GUILayoutUtility.GetLastRect(), texture);
        }
        _itemTarget.ItemImage = (Sprite)EditorGUILayout.ObjectField(_itemTarget.ItemImage, typeof(Sprite), false);
        EditorGUILayout.EndVertical();
    }

    protected void Description()
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Description: ", GUILayout.ExpandWidth(false), GUILayout.Width(70));
        _itemTarget.Description = EditorGUILayout.TextArea(_itemTarget.Description, GUILayout.MinWidth(Screen.width / 2));
        EditorGUILayout.EndHorizontal();
    }

    protected void ShowWeight()
    {
        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        EditorGUILayout.LabelField("Weight: ", GUILayout.ExpandWidth(false), GUILayout.Width(40));
        _itemTarget.ItemWeight = EditorGUILayout.FloatField(_itemTarget.ItemWeight, GUILayout.MinWidth(20), GUILayout.MaxWidth(40));
        EditorGUILayout.LabelField("Kg", GUILayout.Width(20));
        EditorGUILayout.EndHorizontal();
    }
    protected void ShowDurability()
    {
        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        EditorGUILayout.LabelField("Durability: ", GUILayout.ExpandWidth(false), GUILayout.Width(60));
        _itemTarget.Durability = EditorGUILayout.IntField(_itemTarget.Durability, GUILayout.MinWidth(20), GUILayout.MaxWidth(40));
        EditorGUILayout.EndHorizontal();
    }
}
