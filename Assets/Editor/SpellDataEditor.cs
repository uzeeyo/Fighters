#if UNITY_EDITOR
using System.Text.RegularExpressions;
using Fighters.Buffs;
using Fighters.Match;
using Fighters.Match.Spells;
using UnityEditor;
using UnityEngine;
using UnityEngine.VFX;

namespace Editor
{
    [CustomEditor(typeof(SpellData), true)]
    [CanEditMultipleObjects]
    public class SpellDataEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            var spellData = (SpellData)target;

            DrawCommonFields(spellData);
            CheckTargetingChange(spellData);
            CheckBuffFields(spellData);
            serializedObject.ApplyModifiedProperties();
        }

        private void DrawCommonFields(SpellData spellData)
        {
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            serializedObject.FindProperty(GetPropertyName(nameof(spellData.Icon))).objectReferenceValue =
                EditorGUILayout.ObjectField(string.Empty, spellData.Icon, typeof(Sprite), false, GUILayout.Height(100));
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            serializedObject.FindProperty(GetPropertyName(nameof(spellData.Name))).stringValue =
                EditorGUILayout.TextField("Name", spellData.Name);
            serializedObject.FindProperty(GetPropertyName(nameof(spellData.Description))).stringValue =
                EditorGUILayout.TextField("Description", spellData.Description);
            serializedObject.FindProperty(GetPropertyName(nameof(spellData.AnimationName))).stringValue =
                EditorGUILayout.TextField("AnimationName", spellData.AnimationName);
            serializedObject.FindProperty(GetPropertyName(nameof(spellData.ManaCost))).floatValue =
                EditorGUILayout.FloatField("ManaCost", spellData.ManaCost);
            serializedObject.FindProperty(GetPropertyName(nameof(spellData.Cooldown))).floatValue =
                EditorGUILayout.FloatField("Cooldown", spellData.Cooldown);
            serializedObject.FindProperty(GetPropertyName(nameof(spellData.CastTime))).floatValue =
                EditorGUILayout.FloatField("CastTime", spellData.CastTime);
            serializedObject.FindProperty(GetPropertyName(nameof(spellData.Prefab))).objectReferenceValue =
                EditorGUILayout.ObjectField("Prefab", spellData.Prefab, typeof(Spell), false);
            serializedObject.FindProperty(GetPropertyName(nameof(spellData.SpawnLocation))).enumValueIndex =
                (int)(SpawnLocation)EditorGUILayout.EnumPopup("SpawnLocation", spellData.SpawnLocation);

            if (spellData is HealData healData)
            {
                serializedObject.FindProperty(GetPropertyName(nameof(healData.HealAmount))).floatValue =
                    EditorGUILayout.FloatField("Heal Amount", healData.HealAmount);
            }

            if (spellData is DamageData damageData)
            {
                serializedObject.FindProperty(GetPropertyName(nameof(damageData.DamageAmount))).floatValue =
                    EditorGUILayout.FloatField("Damage Amount", damageData.DamageAmount);
                serializedObject.FindProperty(GetPropertyName(nameof(damageData.HitEffect))).objectReferenceValue =
                    EditorGUILayout.ObjectField("Hit Effect", damageData.HitEffect, typeof(VisualEffectAsset), false);
            }
        }

        private void CheckTargetingChange(SpellData spellData)
        {
            EditorGUILayout.Space();

            var propName = GetPropertyName(nameof(spellData.TargetType));
            serializedObject.FindProperty(propName).enumValueFlag =
                (int)(TargetType)EditorGUILayout.EnumPopup("Target Type", spellData.TargetType);
            EditorUtility.SetDirty(spellData);

            CreateTargetingFields(spellData);
        }

        private void CreateTargetingFields(SpellData data)
        {
            if (data.TargetType == TargetType.Self || data.TargetType == TargetType.SingleRandom) return;

            GUILayout.Label($"{data.TargetType} Target Data", new GUIStyle()
            {
                alignment = TextAnchor.MiddleCenter,
                fontSize = 16,
                margin = new RectOffset(0, 0, 20, 5),
                normal = new GUIStyleState { textColor = Color.white }
            });

            var propName = string.Empty;
            switch (data.TargetType)
            {
                case TargetType.Single:
                    propName = GetPropertyName(nameof(SpellData.Range));
                    serializedObject.FindProperty(propName).intValue =
                        EditorGUILayout.IntField("Range", data.Range);
                    break;
                case TargetType.SingleRandom:
                    propName = GetPropertyName(nameof(SpellData.TargetSide));
                    serializedObject.FindProperty(propName).enumValueIndex =
                        (int)(Side)EditorGUILayout.EnumPopup("TargetSide", data.TargetSide);
                    break;
                case TargetType.MultiMoveDelayed:
                    propName = GetPropertyName(nameof(SpellData.TravelTime));
                    serializedObject.FindProperty(propName).floatValue =
                        EditorGUILayout.FloatField("Travel time", data.TravelTime);
                    propName = GetPropertyName(nameof(SpellData.TargetSide));
                    serializedObject.FindProperty(propName).enumValueIndex =
                        (int)(Side)EditorGUILayout.EnumPopup("TargetSide", data.TargetSide);
                    propName = GetPropertyName(nameof(SpellData.RandomTimeInterval));
                    serializedObject.FindProperty(propName).floatValue =
                        EditorGUILayout.FloatField("Time interval", data.RandomTimeInterval);
                    propName = GetPropertyName(nameof(SpellData.Range));
                    serializedObject.FindProperty(propName).intValue =
                        EditorGUILayout.IntField("Tile count", data.Range);
                    propName = GetPropertyName(nameof(SpellData.SpeedCurve));
                    serializedObject.FindProperty(propName).animationCurveValue =
                        EditorGUILayout.CurveField("SpeedCurve", data.SpeedCurve);
                    break;
                case TargetType.MultiForward:
                    propName = GetPropertyName(nameof(SpellData.Range));
                    serializedObject.FindProperty(propName).intValue =
                        EditorGUILayout.IntField("Range", data.Range);
                    break;
                case TargetType.MoveForward:
                    serializedObject.FindProperty(GetPropertyName(nameof(SpellData.TravelTime))).floatValue =
                        EditorGUILayout.FloatField("TravelTime", data.TravelTime);
                    serializedObject.FindProperty((GetPropertyName(nameof(SpellData.HorizontalCurve))))
                            .animationCurveValue =
                        EditorGUILayout.CurveField("HorizontalCurve", data.HorizontalCurve);
                    break;
            }

            propName = GetPropertyName(nameof(SpellData.HasDuration));
            serializedObject.FindProperty(propName).boolValue =
                EditorGUILayout.Toggle("Has Duration", data.HasDuration);
            if (data.HasDuration)
            {
                propName = GetPropertyName(nameof(SpellData.Duration));
                serializedObject.FindProperty(propName).floatValue =
                    EditorGUILayout.FloatField("Duration", data.Duration);
            }
        }

        private void CheckBuffFields(SpellData spellData)
        {
            if (spellData is not BuffData buffData) return;

            GUILayout.Label("Buff Data", new GUIStyle()
            {
                alignment = TextAnchor.MiddleCenter,
                fontSize = 16,
                margin = new RectOffset(0, 0, 20, 5),
                normal = new GUIStyleState { textColor = Color.white }
            });
            
            serializedObject.FindProperty(GetPropertyName(nameof(buffData.BuffType))).enumValueIndex =
                (int)(BuffType)EditorGUILayout.EnumPopup("BuffType", buffData.BuffType);
            serializedObject.FindProperty(GetPropertyName(nameof(buffData.Duration))).floatValue =
                EditorGUILayout.FloatField("Duration", buffData.Duration);
            
            switch (buffData.BuffType)
            {
                case BuffType.Poison:
                {
                    serializedObject.FindProperty(GetPropertyName(nameof(buffData.HPPS))).floatValue =
                        EditorGUILayout.FloatField("DPS", buffData.HPPS);
                    break;
                }
            }
        }

        private static string GetPropertyName(string propertyName)
        {
            const string pattern = @"\b([A-Z][a-zA-Z0-9]*)\b";
            return Regex.Replace(propertyName, pattern, m => $"_{char.ToLower(m.Value[0])}{m.Value.Substring(1)}");
        }
    }
}
#endif