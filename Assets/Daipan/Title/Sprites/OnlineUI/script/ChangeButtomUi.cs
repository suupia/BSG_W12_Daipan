using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ChangeButtomUi : MonoBehaviour
{
    [SerializeField]
    Image Button = null!;
    [SerializeField]
    Sprite readyUi=null!;
    [SerializeField]
    Sprite OkUi=null!;
    private bool flg = true;

    private void Start()
    {
    }

    public void OnClick()
    {
        Debug.Log("hoge");
        Button.sprite= (flg) ? OkUi : readyUi;
        flg = !flg;
    }
}
