using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorPicker : MonoBehaviour
{
    public Image image;
    public Slider slider_R;
    public Slider slider_G;
    public Slider slider_B;
    public Slider slider_A;

    void Update()
    {
        image.color = new Color(slider_R.value, slider_G.value, slider_B.value, slider_A.value);
    }
}
