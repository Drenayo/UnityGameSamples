using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    // 【使用说明】：参考了网上代码 ~~
    // 1、拖拽到摄像头 作为摄像头的组件
    // 2、运行的时候：
    // （1）镜头旋转 右键点击着屏幕 然后移动鼠标 即可旋转屏幕
    // （2）镜头缩放 滚轮控制缩放镜头
    // （3）手型工具 就像scene里面的手型工具一样 滚轮点击着拖拽屏幕
    // （4）镜头复原 空格键就会复用镜头

    ///【1】用于计算的变量
    //旋转变量;
    private float m_deltX = 0f;
    private float m_deltY = 0f;
    //摄像机原始位置 和 旋转角度  给复原使用
    private Vector3 m_vecOriPosition;
    private Quaternion m_vecOriRotation;
    //手型工具：上次点击屏幕的位置
    private Vector3 m_vecLasMouseClickPosition;

    ///【2】用于控制幅度的变量
    //缩放幅度;
    public float m_fScalingSpeed = 10f;
    //镜头旋转幅度;
    public float m_fRotateSpeed = 5f;
    //手型工具幅度;
    public float m_fHandToolSpeed = -0.005f;

    void Start()
    {
        m_vecOriRotation = GetComponent<Camera>().transform.rotation;
        m_vecOriPosition = GetComponent<Camera>().transform.position;
    }

    void Update()
    {
        //（1）旋转镜头 鼠标右键点下控制相机旋转;
        if (Input.GetMouseButton(1))
        {
            m_deltX += Input.GetAxis("Mouse X") * m_fRotateSpeed;
            m_deltY -= Input.GetAxis("Mouse Y") * m_fRotateSpeed;
            m_deltX = ClampAngle(m_deltX, -360, 360);
            m_deltY = ClampAngle(m_deltY, -70, 70);
            GetComponent<Camera>().transform.rotation = Quaternion.Euler(m_deltY, m_deltX, 0);
        }

        //（2）镜头缩放
        //鼠标中键点下场景缩放;
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            //自由缩放方式;
            m_fScalingSpeed = Input.GetAxis("Mouse ScrollWheel") * 10f;
            GetComponent<Camera>().transform.localPosition = GetComponent<Camera>().transform.position + GetComponent<Camera>().transform.forward * m_fScalingSpeed;
        }
         
        //（3）手型工具
        if (Input.GetMouseButtonDown(2))
        {
            m_vecLasMouseClickPosition = Input.mousePosition;
        }
        else if (Input.GetMouseButton(2))
        {
            Vector3 NowHitPosition = Input.mousePosition;
            Vector3 offsetVec = NowHitPosition - m_vecLasMouseClickPosition;
            offsetVec = GetComponent<Camera>().transform.rotation * offsetVec;
            GetComponent<Camera>().transform.localPosition = GetComponent<Camera>().transform.localPosition + offsetVec * (m_fHandToolSpeed);
            m_vecLasMouseClickPosition = Input.mousePosition;
        }

        //(4)相机复位远点;
        if (Input.GetKey(KeyCode.Space))
        {
            m_deltX = 0f;
            m_deltY = 0f;
            m_deltX = ClampAngle(m_deltX, -360, 360);
            m_deltY = ClampAngle(m_deltY, -70, 70);
            m_fScalingSpeed = 10.0f;
            GetComponent<Camera>().transform.rotation = m_vecOriRotation;
            GetComponent<Camera>().transform.localPosition = m_vecOriPosition;
        }
    }

    //规划角度;
    float ClampAngle(float angle, float minAngle, float maxAgnle)
    {
        if (angle <= -360)
            angle += 360;
        if (angle >= 360)
            angle -= 360;

        return Mathf.Clamp(angle, minAngle, maxAgnle);
    }
}