using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransition:MonoBehaviour
{
    Button button;
    [SerializeField]
    private string SceneName;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() => SceneManager.LoadScene(SceneName));
    }
}