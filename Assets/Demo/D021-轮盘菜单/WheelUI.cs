using UnityEngine;
using UnityEngine.UI;

public class WheelUI : MonoBehaviour
{
    public RectTransform wheelRect; // 轮盘的RectTransform
    public float wheelRadius = 200f; // 轮盘半径
    public int numSlices = 8; // 扇形区域数量
    public float scaleFactorOnHover = 1.2f; // 鼠标悬停时的缩放比例
    public Sprite baseSliceSprite; // 基础扇形图片

    private Image[] sliceImages; // 扇形区域的Image组件
    private Text[] sliceTexts; // 扇形区域的文本
    private int currentHoverSlice = -1; // 当前鼠标悬停的扇形区域序号

    private void Start()
    {
        // 创建扇形区域和文本
        sliceImages = new Image[numSlices];
        sliceTexts = new Text[numSlices];
        for (int i = 0; i < numSlices; i++)
        {
            sliceImages[i] = CreateSliceImage(i);
            sliceTexts[i] = CreateSliceText(i);
        }
    }

    private void Update()
    {
        // 检测Tab键和鼠标位置
        if (Input.GetKey(KeyCode.Tab))
        {
            int hoverSlice = GetHoverSlice(Input.mousePosition);
            if (hoverSlice != currentHoverSlice)
            {
                if (currentHoverSlice != -1)
                    ResetSliceScale(currentHoverSlice);
                if (hoverSlice != -1)
                    ScaleSlice(hoverSlice);
                currentHoverSlice = hoverSlice;
            }
        }
        else
        {
            if (currentHoverSlice != -1)
            {
                ResetSliceScale(currentHoverSlice);
                currentHoverSlice = -1;
                PrintSliceNumber();
            }
        }
    }

    private Image CreateSliceImage(int sliceIndex)
    {
        GameObject sliceObj = new GameObject("Slice" + sliceIndex);
        sliceObj.transform.SetParent(wheelRect.transform, false);
        Image sliceImage = sliceObj.AddComponent<Image>();
        sliceImage.sprite = baseSliceSprite;
        SetSliceTransform(sliceImage.rectTransform, sliceIndex);
        return sliceImage;
    }

    private Text CreateSliceText(int sliceIndex)
    {
        GameObject textObj = new GameObject("SliceText" + sliceIndex);
        textObj.transform.SetParent(sliceImages[sliceIndex].transform, false);
        Text text = textObj.AddComponent<Text>();
        text.text = sliceIndex.ToString();
        text.alignment = TextAnchor.MiddleCenter;
        return text;
    }

    private void SetSliceTransform(RectTransform sliceRect, int sliceIndex)
    {
        float angleRange = 360f / numSlices;
        float startAngle = sliceIndex * angleRange;
        float endAngle = startAngle + angleRange;
        float sliceAngleRadians = Mathf.Deg2Rad * angleRange;

        sliceRect.pivot = new Vector2(0.5f, 0.5f);
        sliceRect.anchorMin = Vector2.zero;
        sliceRect.anchorMax = Vector2.one;
        sliceRect.anchoredPosition = Vector2.zero;
        sliceRect.sizeDelta = new Vector2(Mathf.Sin(sliceAngleRadians) * wheelRadius * 2, wheelRadius * 2);
        sliceRect.localRotation = Quaternion.Euler(0, 0, -startAngle);
        sliceRect.localScale = Vector3.one * 0.1f;
    }


    private void ScaleSlice(int sliceIndex)
    {
        sliceImages[sliceIndex].transform.localScale = Vector3.one * scaleFactorOnHover;
    }

    private void ResetSliceScale(int sliceIndex)
    {
        sliceImages[sliceIndex].transform.localScale = Vector3.one;
    }

    private int GetHoverSlice(Vector3 mousePosition)
    {
        Vector2 localMousePosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(wheelRect, mousePosition, null, out localMousePosition);
        float angle = Mathf.Atan2(localMousePosition.y, localMousePosition.x) * Mathf.Rad2Deg;
        angle = (angle + 360f) % 360f;
        float angleRange = 360f / numSlices;
        int sliceIndex = Mathf.FloorToInt(angle / angleRange);
        if (localMousePosition.magnitude > wheelRadius)
            return -1;
        return sliceIndex;
    }

    private void PrintSliceNumber()
    {
        if (currentHoverSlice != -1)
            Debug.Log("Selected Slice: " + currentHoverSlice);
    }
}