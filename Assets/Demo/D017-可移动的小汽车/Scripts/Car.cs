using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace D017
{
    /// <summary>
    /// 车辆控制
    /// </summary>
    public class Car : MonoBehaviour
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
        void Update()
        {
            WheelsControl_Update();
            WheelRot();
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
                //Debug.Log(leftF.steerAngle);
                leftF.steerAngle -= Time.deltaTime * angleSpeed;
                rightF.steerAngle -= Time.deltaTime * angleSpeed;
                if (leftF.steerAngle < (0 - maxAngle) || rightF.steerAngle < (0 - maxAngle))
                {
                    //到最大值后就不能继续加角度了
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
    }
}

