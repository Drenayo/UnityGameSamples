using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RadialSlider : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    bool isPointerDown = false; // 鼠标是否按下

    // 开始跟踪鼠标
    public void OnPointerEnter(PointerEventData eventData)
    {
        StartCoroutine("TrackPointer");
    }

    // 停止跟踪鼠标
    public void OnPointerExit(PointerEventData eventData)
    {
        StopCoroutine("TrackPointer");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isPointerDown = true;
        //Debug.Log("mousedown");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPointerDown = false;
        //Debug.Log("mousedown");
    }

    // 主循环
    IEnumerator TrackPointer()
    {
        var ray = GetComponentInParent<GraphicRaycaster>();
        var input = FindObjectOfType<StandaloneInputModule>();

        var text = GetComponentInChildren<Text>();

        if (ray != null && input != null)
        {
            while (Application.isPlaying)
            {
                // 如果鼠标按下
                if (isPointerDown)
                {
                    Vector2 localPos; // 鼠标位置

                    // 将屏幕坐标转换为相对于目标RectTransform的本地坐标
                    RectTransformUtility.ScreenPointToLocalPointInRectangle(transform as RectTransform, Input.mousePosition, ray.eventCamera, out localPos);

                    // localPos现在是鼠标在目标RectTransform的本地坐标
                    // 利用三角函数计算鼠标位置与原点的夹角
                    // Atan2计算反正切值,返回角度的弧度制值
                    // 由于Atan2的返回值范围是(-PI,PI],需要加上180将其转换到(0,360]的范围
                    // 然后除以360得到0到1之间的相对值,代表进度条的填充百分比
                    float angle = (Mathf.Atan2(-localPos.y, localPos.x) * 180f / Mathf.PI + 180f) / 360f;

                    // 设置Image组件的fillAmount属性,控制进度条的填充量
                    GetComponent<Image>().fillAmount = angle;

                    // 使用Color.Lerp插值函数根据进度条的填充量在绿色和红色之间计算出一种颜色
                    // 当angle为0时,颜色为绿色;当angle为1时,颜色为红色
                    GetComponent<Image>().color = Color.Lerp(Color.green, Color.red, angle);

                    // 将填充量转换为角度值(0到360度),并转换为字符串显示在Text组件上
                    text.text = ((int)(angle * 360f)).ToString();

                    //Debug.Log(localPos+" : "+angle);
                }
                yield return 0;
            }
        }
        else
            UnityEngine.Debug.LogWarning("Could not find GraphicRaycaster and/or StandaloneInputModule");
    }
}