using UnityEngine;

namespace D018
{
    public class ClikeMove : MonoBehaviour
    {
        public float groundClearance = 0.5f;
        public float speed = 3.0f; // 新增的速度变量
        private bool isNextMove = false;
        private Vector3 point;

        void Update()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Input.GetMouseButtonDown(0))
            // 当鼠标点击时，才触发射线检测
            {
                if (Physics.Raycast(ray, out hitInfo))
                // 当检测到地面
                {
                    isNextMove = true;
                    point = hitInfo.point;
                    // 将isNextMove设为true，然后保存当前撞击点位置

                    // 新增的转向代码
                    Vector3 direction = (point - transform.position).normalized;
                    Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
                    transform.rotation = lookRotation;
                    // 计算转向目标方向，并更新对象的旋转
                }
            }
            if (isNextMove == true)
            // 当isNextMove为真，则不停调用Move
            {
                Move(point);
            }
        }

        void Move(Vector3 pos)
        {
            // 使用Vector3的插值函数来移动位置
            transform.position = Vector3.MoveTowards(transform.position, pos + new Vector3(0, groundClearance, 0), Time.deltaTime * speed);
            if (transform.position == pos + new Vector3(0, groundClearance, 0))
            // 当目标抵达位置的时候，将isNextMove置为false，等待下一次移动指令
            {
                isNextMove = false;
            }
        }
    }
}
