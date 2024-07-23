using UnityEngine;


namespace D027
{
    public class PointHandle : MonoBehaviour
    {
        private Transform targetTrans;
        private void Update()
        {
            // 鼠标左键按下
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    targetTrans = hit.transform;
                }
            }
            // 鼠标左键抬起
            if (Input.GetMouseButtonUp(0))
            {
                targetTrans = null;
            }

            // 鼠标按住中
            if (targetTrans != null && Input.GetMouseButton(0))
            {
                // 获取鼠标在屏幕空间的坐标
                Vector3 mouseScreenPosition = Input.mousePosition;

                // 获取 targetTrans 在屏幕空间的 z 坐标
                float targetZ = Camera.main.WorldToScreenPoint(targetTrans.position).z;

                // 将鼠标的屏幕坐标转换为世界坐标
                Vector3 targetWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mouseScreenPosition.x, mouseScreenPosition.y, targetZ));

                // 更新 targetTrans 的位置为转换后的世界坐标
                targetTrans.position = targetWorldPosition;
            }
        }
    }


}
