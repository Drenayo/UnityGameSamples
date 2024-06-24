using UnityEngine;

namespace S001
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteOutline : MonoBehaviour
    {
        public float outlineWidth = 1f;
        public Color outlineColor = Color.white;

        private SpriteRenderer spriteRenderer;
        private Material outlineMaterial;

        void Start()
        {
            EnableOutLine(outlineWidth, outlineColor);
        }

        void OnValidate()
        {
            if (outlineMaterial != null)
            {
                outlineMaterial.SetFloat("_lineWidth", outlineWidth);
                outlineMaterial.SetColor("_lineColor", outlineColor);
            }
        }

        /// <summary>
        /// 启用描边效果
        /// </summary>
        /// <param name="width">描边宽度</param>
        /// <param name="color">描边颜色</param>
        public void EnableOutLine(float width, Color color)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            if (spriteRenderer == null)
            {
                Debug.LogError("SpriteRenderer未找到！");
                return;
            }

            // 创建新的材质实例，应用描边效果
            outlineMaterial = new Material(Shader.Find("shader2D/outline"));
            spriteRenderer.material = outlineMaterial;

            outlineMaterial.SetFloat("_lineWidth", width);
            outlineMaterial.SetColor("_lineColor", color);
        }


        /// <summary>
        /// 禁用描边效果
        /// </summary>
        public void DisableOutLine()
        {
            outlineMaterial = new Material(Shader.Find("Sprites/Default"));
            spriteRenderer.material = outlineMaterial;
        }
    }
}
