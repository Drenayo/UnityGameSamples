using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Z1
{
    public class GUITest : MonoBehaviour
    {
        private void OnGUI()
        {
            GUISkin skin = new GUISkin();
            GUIStyle style = new GUIStyle();
            skin.box = style;

            GUI.Button(new Rect(0, 0, 140, 80), "Start",style);
        }
    }
}
