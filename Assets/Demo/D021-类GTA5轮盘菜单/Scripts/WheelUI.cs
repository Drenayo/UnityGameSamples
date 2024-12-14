using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class WheelUI : MonoBehaviour
{
    [Header("轮盘UI根物体")]
    public GameObject wheelMenu;
    [Header("轮盘选项数量")]
    public int numberOfOptions = 6;
    [Header("选项预设")]
    public GameObject optionPrefab;
    [Header("轮盘半径")]
    public float wheelRadius = 200f;
    [Header("快捷键")]
    public KeyCode toggleKey = KeyCode.Tab;

    [Header("被选中的选项")]
    public string chosenOption;

    private RectTransform[] options;     // 选项数组
    private bool isMenuVisible = false;  // 轮盘是否可见

    void Start()
    {
        // 初始化选项容器
        options = new RectTransform[numberOfOptions];

        // 动态创建选项
        for (int i = 0; i < numberOfOptions; i++)
        {
            GameObject option = Instantiate(optionPrefab, wheelMenu.transform);
            option.name = "option" + i;
            RectTransform rt = option.GetComponent<RectTransform>();
            options[i] = rt;

            // 计算每个选项的角度  
            float angle = i * (360f / numberOfOptions);

            // 位置的角度偏移90°，默认从水平右偏移到垂直上
            float positionAngle = angle + 90;
            
            Vector3 position = new Vector3(
                Mathf.Cos(Mathf.Deg2Rad * positionAngle) * wheelRadius, 
                Mathf.Sin(Mathf.Deg2Rad * positionAngle) * wheelRadius, 
                0);

            rt.localPosition = position;
            rt.localRotation = Quaternion.Euler(0, 0, angle);  // 设置每个选项的旋转角度
        }

        // 隐藏轮盘
        wheelMenu.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKey(toggleKey))
        {
            wheelMenu.SetActive(true);
            isMenuVisible = true;
        }
        else
        {
            wheelMenu.SetActive(false);

            if (isMenuVisible)
            {
                // 松开轮盘时，执行被选中的选项的操作
                Debug.Log(chosenOption);
            }
                
            isMenuVisible = false;
        }

        // 如果轮盘显示，监听鼠标位置
        if (isMenuVisible)
        {
            Vector2 mousePos = Input.mousePosition;
            Vector2 wheelCenter = wheelMenu.transform.position;

            // 计算鼠标与轮盘中心的角度
            Vector2 direction = mousePos - wheelCenter;
            // 弧度转角度
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            float adjustedAngle = angle - 90; // 加回偏移的 90°

            // 确保角度在 0° 到 360° 之间
            if (adjustedAngle < 0)
                adjustedAngle += 360f;

            // 高亮当前选项
            HighlightOption(adjustedAngle);
        }
    }

    // 高亮当前选中的扇形选项
    void HighlightOption(float angle)
    {
        int selectedIndex = Mathf.FloorToInt(angle / (360f / numberOfOptions)) % numberOfOptions;

        // 高亮选项（可以更改UI样式来显示高亮效果）
        for (int i = 0; i < numberOfOptions; i++)
        {
            options[i].GetComponent<Image>().color = (i == selectedIndex) ? Color.yellow : Color.white;
            chosenOption = options[selectedIndex].name;
        }
    }
}
