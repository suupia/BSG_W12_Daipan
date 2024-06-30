using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpritesCamera : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var camera = GetComponent<Camera>();

        camera.transparencySortMode = TransparencySortMode.CustomAxis;

        camera.transparencySortAxis = new Vector3(0.0f, 1.0f, 0.0f);
    }
}
