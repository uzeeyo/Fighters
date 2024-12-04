#if UNITY_EDITOR
using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;
using Fighters.Buffs;
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
        private enum ShakeType
        {
            None,
            OnCast,
            OnImpact,
        }
        
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
            GetProperty(nameof(spellData.Icon)).objectReferenceValue =
                EditorGUILayout.ObjectField(string.Empty, spellData.Icon, typeof(Sprite), false, GUILayout.Height(100));
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

            DrawField("Name", x => x.Name);
            DrawField("Description", x => x.Description);
            DrawField("AnimationName", x => x.AnimationName);
            DrawField("ManaCost", x => x.ManaCost);
            DrawField("Cooldown", x => x.Cooldown);
            DrawField("CastTime", x => x.CastTime);
            DrawField("Prefab", x => x.Prefab);
            DrawField("SpawnLocation", x => x.SpawnLocation);
            DrawField("Shakes on impact", x => x.ShakesOnImpact);
            DrawField("Shakes on cast", x => x.ShakesOnCast);

            if (spellData.ShakesOnImpact)
            {
                GetProperty(nameof(spellData.ShakesOnCast)).boolValue = false;
            }

            if (spellData.ShakesOnCast)
            {
                GetProperty(nameof(spellData.ShakesOnImpact)).boolValue = false;
            }

            if (spellData.ShakesOnImpact || spellData.ShakesOnCast)
            {
                DrawField("Shake Duration", x => x.ShakeDuration);
                GetProperty(nameof(spellData.ShakeStrength)).floatValue = EditorGUILayout.Slider("Shake Strength", spellData.ShakeStrength, 0f, 0.5f);
            }

            if (spellData is HealData healData)
            {
                DrawField("Heal Amount", x => healData.HealAmount);
            }

            if (spellData is DamageData damageData)
            {
                DrawField("Damage Amount", x => damageData.DamageAmount);
                DrawField("Hit Effect", x => damageData.HitEffect);
            }
        }

        private void CheckTargetingChange(SpellData spellData)
        {
            EditorGUILayout.Space();
            DrawField("Target Type", x => x.TargetType);
            EditorUtility.SetDirty(spellData);

            CreateTargetingFields(spellData);
        }

        private void CreateTargetingFields(SpellData data)
        {
            if (data.TargetType is TargetType.Self or TargetType.SingleRandom) return;

            GUILayout.Label($"{data.TargetType} Target Data", new GUIStyle()
            {
                alignment = TextAnchor.MiddleCenter,
                fontSize = 16,
                margin = new RectOffset(0, 0, 20, 5),
                normal = new GUIStyleState { textColor = Color.white }
            });

            switch (data.TargetType)
            {
                case TargetType.Single:
                    DrawField("Range", x => x.Range);
                    break;
                case TargetType.SingleRandom:
                    DrawField("TargetSide", x => x.TargetSide);
                    break;
                case TargetType.MultiMoveDelayed:
                    DrawField("Travel Time", x => x.TravelTime);
                    DrawField("TargetSide", x => x.TargetSide);
                    DrawField("Time Interval", x => x.RandomTimeInterval);
                    DrawField("Tile Count", x => x.Range);
                    DrawField("Speed Curve", x => x.SpeedCurve);
                    break;
                case TargetType.MultiForward:
                    DrawField("Range", x => x.Range);
                    break;
                case TargetType.MoveForward:
                    DrawField("Travel Time", x => x.TravelTime);
                    DrawField("Horizontal Curve", x => x.HorizontalCurve);
                    break;
            }

            DrawField("Has Duration", x => x.HasDuration);
            if (data.HasDuration)
            {
                DrawField("Duration", x => x.Duration);
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
            
            DrawField("Buff Type", x => buffData.BuffType);
            DrawField("Duration", x => buffData.Duration);
            
            switch (buffData.BuffType)
            {
                case BuffType.Poison:
                {
                    DrawField("DPS", x => buffData.HPPS);
                    break;
                }
            }
        }

        private SerializedProperty GetProperty(string propertyName)
        {
            const string pattern = @"\b([A-Z][a-zA-Z0-9]*)\b";
            var privateName = Regex.Replace(propertyName, pattern, m => $"_{char.ToLower(m.Value[0])}{m.Value.Substring(1)}");
            return serializedObject.FindProperty(privateName);
        }
        
        private void DrawField<T>(string label, Expression<Func<SpellData, T>> propertyExpression)
        {
            var memberExpression = propertyExpression.Body as MemberExpression;
            if (memberExpression == null) return;
            var propertyInfo = memberExpression.Member as PropertyInfo;

            if (propertyInfo == null) return;
            var prop = GetProperty(propertyInfo.Name);
            EditorGUILayout.PropertyField(prop, new GUIContent(label));
        }
    }
}
#endif