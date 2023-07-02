using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Z2_1
{
    public class UISetting : MonoBehaviour
    {
        public SortSetting sortSetting;
        public Slider sliderSpeed;
        public Dropdown dropdownSortType;

        private void Start()
        {
            sliderSpeed.value = sortSetting.sortspeed;
        }

        public void Slider_Speed()
        {
            sortSetting.sortspeed = sliderSpeed.value;
        }
        public void Dropdown_SortType()
        {
            switch (dropdownSortType.value)
            {
                case 0:
                    sortSetting.sortType = SortType.冒泡;
                    break;
                case 1:
                    sortSetting.sortType = SortType.选择;
                    break;
                case 2:
                    sortSetting.sortType = SortType.插入;
                    break;
                default:
                    break;
            }
        }
    }
}
