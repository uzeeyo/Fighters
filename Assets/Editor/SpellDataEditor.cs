#if UNITY_EDITOR
using Fighters.Match.Spells;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(SpellData), true)]
    public class SpellDataEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            
            var spellData = (SpellData)target;

            CheckTargetingChange(spellData);
        }

        private void CheckTargetingChange(SpellData spellData)
        {
            EditorGUI.BeginChangeCheck();
            var targetType = (TargetType)EditorGUILayout.EnumPopup("Target Type", spellData.TargetType);

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(spellData, "Change Spell Type");
                spellData.TargetType = targetType;
                EditorUtility.SetDirty(spellData);
            }
            
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
            
            switch (data.TargetType)
            {
                case TargetType.Single:
                    EditorGUI.BeginChangeCheck();
                    var spacing = EditorGUILayout.IntField("Range", data.Range);
                    if (EditorGUI.EndChangeCheck())
                    {
                        Undo.RecordObject(data, "Change Forward Spacing");
                        data.Range = spacing;
                        EditorUtility.SetDirty(data);
                    }
                    break;
                case TargetType.MultiRandom:
                    EditorGUI.BeginChangeCheck();
                    var interval = EditorGUILayout.FloatField("Time interval", data.RandomTimeInterval);
                    if (EditorGUI.EndChangeCheck())
                    {
                        Undo.RecordObject(data, "Change Random Time");
                        data.RandomTimeInterval = interval;
                        EditorUtility.SetDirty(data);
                    }
                    break;
            }
        }
    }
}
#endif