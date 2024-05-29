using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition:MonoBehaviour
{
    [SerializeField]
    private string[] SceneName;
    [SerializeField]
    private bool[] scene;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && scene[0])
        {
            SceneManager.LoadScene(SceneName[0]);
        }
    }
}