using TMPro;
using UnityEngine;

namespace Extensions
{
    public static class TMProExtensions
    {
        private static readonly int UnderlayColor = Shader.PropertyToID("_UnderlayColor");
        private static readonly int UnderlayDilate = Shader.PropertyToID("_UnderlayDilate");
        private static readonly int UnderlaySoftness = Shader.PropertyToID("_UnderlaySoftness");

        public static void Outline(this TextMeshProUGUI textMesh, Color color, float size)
        {
            var dilation = Mathf.Clamp(size, 0, 1);
            textMesh.material.SetColor(UnderlayColor, color);
            textMesh.material.SetFloat(UnderlayDilate, dilation);
            textMesh.material.SetFloat(UnderlaySoftness, 0.1f);
        }
    }
}