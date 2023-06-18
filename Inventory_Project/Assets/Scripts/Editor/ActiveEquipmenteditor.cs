using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ActiveEquipment))]
public class ActiveEquipmentEditor : EquipmentEditor
{

    ActiveEquipment m_Equipment;

    protected override void OnEnable()
    {
        base.OnEnable();
        m_Equipment = (ActiveEquipment)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        EditorGUI.BeginChangeCheck();
        ShowAmmunition();
        ShowPrefab();
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(_itemTarget, "Weapon or Shield changed");
            EditorUtility.SetDirty(_itemTarget);
        }
    }
    void ShowPrefab()
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Weapon prefab: ");
        m_Equipment.PrefabMesh = (GameObject)EditorGUILayout.ObjectField(m_Equipment.PrefabMesh, typeof(GameObject), false);
        EditorGUILayout.EndHorizontal();
    }
    void ShowAmmunition()
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Weapon ammunition: ");
        m_Equipment.Ammunition = (Resources)EditorGUILayout.ObjectField(m_Equipment.Ammunition, typeof(Resources), false);
        EditorGUILayout.EndHorizontal();
    }
}
