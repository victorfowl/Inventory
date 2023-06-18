using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Potions))]
public class PotionEditor : ConsumableEditor
{

    Potions _potionTarget;

    protected override void OnEnable()
    {
        base.OnEnable();
        _potionTarget = (Potions)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        ShowPotionType();
        ShowAmmount(_potionTarget.typeOfPotion);
    }

    void ShowPotionType()
    {

        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        EditorGUILayout.LabelField("Type: ", GUILayout.ExpandWidth(false), GUILayout.Width(40));
        _potionTarget.typeOfPotion = (Potions.TypeOfPotion)EditorGUILayout.EnumPopup(_potionTarget.typeOfPotion, GUILayout.MinWidth(70), GUILayout.MaxWidth(90));
        EditorGUILayout.EndHorizontal();
    }
    protected void ShowAmmount(Potions.TypeOfPotion type)
    {
        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        switch (type)
        {
            case Potions.TypeOfPotion.Heal:
                EditorGUILayout.LabelField("Life points to restore: ", GUILayout.ExpandWidth(false), GUILayout.Width(130));
                _potionTarget.Ammount = EditorGUILayout.FloatField(_potionTarget.Ammount, GUILayout.MinWidth(20), GUILayout.MaxWidth(40));
                break;
            case Potions.TypeOfPotion.Mana:
                EditorGUILayout.LabelField("Mana to restore: ", GUILayout.ExpandWidth(false), GUILayout.Width(100));
                _potionTarget.Ammount = EditorGUILayout.FloatField(_potionTarget.Ammount, GUILayout.MinWidth(20), GUILayout.MaxWidth(40));
                break;
            default:
                break;
        }
        EditorGUILayout.EndHorizontal();
    }
}
