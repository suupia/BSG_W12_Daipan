using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace A
{
    public class ChangeButtomUi : MonoBehaviour
    {
        [SerializeField]
        Image button = null!;
        [SerializeField]
        Sprite readyUi = null!;
        [SerializeField]
        Sprite OkUi = null!;
        private bool flg = false;

        private void Start()
        {
        }

        public void OnClick()
        {
            Debug.Log("hoge");
            button.sprite = (flg) ? OkUi : readyUi;
            flg = !flg;
        }
    }
}
