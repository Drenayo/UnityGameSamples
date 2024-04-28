using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace D023
{
    public class UISetting : MonoBehaviour
    {
        public SortSetting sortSetting;
        public Slider sliderSpeed;
        public Dropdown dropdownSortType;

        private void Start()
        {
            sliderSpeed.value = sortSetting.sortspeed;
            sortSetting.sortType = SortType.冒泡;
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
                case 3:
                    sortSetting.sortType = SortType.希尔;
                    break;
                case 4:
                    sortSetting.sortType = SortType.快速;
                    break;
                case 5:
                    sortSetting.sortType = SortType.计数;
                    break;
                case 6:
                    sortSetting.sortType = SortType.桶;
                    break;
                case 7:
                    sortSetting.sortType = SortType.基数;
                    break;
                case 8:
                    sortSetting.sortType = SortType.堆;
                    break;
                case 9:
                    sortSetting.sortType = SortType.归并;
                    break;
                default:
                    break;
            }
        }

    }
}
