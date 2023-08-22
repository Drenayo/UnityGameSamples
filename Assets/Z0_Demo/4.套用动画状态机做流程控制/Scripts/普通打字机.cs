using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class 普通打字机 : StateMachineBehaviour
{
    public Text textComponent;
    public string typeContent;
    public float typeSpeed;
    public bool playOnAwake;
    public UnityEvent aa;
    IEnumerator TypeWriter_E()
    {
        textComponent.text = string.Empty;
        string strTemp = string.Empty;
        for (int i = 0; i < typeContent.Length; i++)
        {
            yield return new WaitForSeconds(typeSpeed);
            strTemp += typeContent[i];
            textComponent.text = strTemp;
        }
        yield break;
    }
    public void StartTypeWriter()
    {
        
    }

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Debug.Log("刚刚进入当前状态");

    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        //Debug.Log("Idel更新状态中！");
    }


    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Debug.Log("退出状态！");
    }
}
