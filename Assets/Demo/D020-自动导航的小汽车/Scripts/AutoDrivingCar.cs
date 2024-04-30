using UnityEngine;
using UnityEngine.AI;

namespace D020
{
    /// <summary>
    /// 车辆控制
    /// </summary>
    public class AutoDrivingCar : MonoBehaviour
    {
        public float moveSpeed = 1500;
        public float maxAngle = 35;
        public float angleSpeed = 50;
        public float breakMove = 1000f;
        public WheelCollider leftF;
        public WheelCollider leftB;
        public WheelCollider rightF;
        public WheelCollider rightB;
        public Transform model_leftF;
        public Transform model_leftB;
        public Transform model_rightF;
        public Transform model_rightB;

        private NavMeshAgent navAgent;
        private bool isAutodriving = false;


        public Transform tragetPos;

        private void Start()
        {
            navAgent = GetComponent<NavMeshAgent>();
            SetDestination(tragetPos.position);
        }

        // 重新寻路
        public void ResetWayFinding()
        {
            // 随机重置目标点位置
            tragetPos.position = new Vector3(UnityEngine.Random.Range(-20, 20), tragetPos.position.y, UnityEngine.Random.Range(-20, 20));
            // 设置点
            SetDestination(tragetPos.position);
        }

        void Update()
        {
            if (isAutodriving)
            {
                AutoDrive();
            }
            else
            {
                WheelsControl_Update();
            }

            // 键盘的操作逻辑
            // WheelRot();
        }

        void WheelsControl_Update()
        {
            //垂直轴和水平轴
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");
            //前轮角度，后轮驱动
            leftB.motorTorque = v * moveSpeed;
            rightB.motorTorque = v * moveSpeed;
            WheelsModel_Update(model_leftF, leftF);
            WheelsModel_Update(model_leftB, leftB);
            WheelsModel_Update(model_rightF, rightF);
            WheelsModel_Update(model_rightB, rightB);
        }

        void WheelsModel_Update(Transform t, WheelCollider wheel)
        {
            Vector3 pos = t.position;
            Quaternion rot = t.rotation;
            wheel.GetWorldPose(out pos, out rot);
            t.position = pos;
            t.rotation = rot;
        }

        void WheelRot()
        {
            //左转向
            if (Input.GetKey(KeyCode.A))
            {
                leftF.steerAngle -= Time.deltaTime * angleSpeed;
                rightF.steerAngle -= Time.deltaTime * angleSpeed;
                if (leftF.steerAngle < (0 - maxAngle) || rightF.steerAngle < (0 - maxAngle))
                {
                    leftF.steerAngle = (0 - maxAngle);
                    rightF.steerAngle = (0 - maxAngle);
                }
            }
            //右转向
            if (Input.GetKey(KeyCode.D))
            {
                leftF.steerAngle += Time.deltaTime * angleSpeed;
                rightF.steerAngle += Time.deltaTime * angleSpeed;
                if (leftF.steerAngle > maxAngle || rightF.steerAngle > maxAngle)
                {
                    leftF.steerAngle = maxAngle;
                    rightF.steerAngle = maxAngle;
                }
            }
            //松开转向后，方向打回
            if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
                leftF.steerAngle = rightF.steerAngle = 0;

            bool isBraking = Input.GetKey(KeyCode.Space);
            leftB.brakeTorque = isBraking ? breakMove : 0f;
            rightB.brakeTorque = isBraking ? breakMove : 0f;
        }


        void AutoDrive()
        {
            // 根据NavMeshAgent的移动方向计算车轮的转向角度
            Vector3 velocity = navAgent.velocity;
            Vector3 forward = transform.forward;
            float angle = Vector3.SignedAngle(forward, velocity, Vector3.up);

            leftF.steerAngle = angle;
            rightF.steerAngle = angle;

            // 使用NavMeshAgent的速度来控制车轮转速
            float agentSpeed = navAgent.velocity.magnitude;
            float motorTorque = agentSpeed * moveSpeed;

            leftF.motorTorque = motorTorque;
            rightF.motorTorque = motorTorque;
            leftB.motorTorque = motorTorque;
            rightB.motorTorque = motorTorque;

            WheelsModel_Update(model_leftF, leftF);
            WheelsModel_Update(model_leftB, leftB);
            WheelsModel_Update(model_rightF, rightF);
            WheelsModel_Update(model_rightB, rightB);
        }

        public void SetDestination(Vector3 destination)
        {
            navAgent.SetDestination(destination);
            isAutodriving = true;
        }
    }
}