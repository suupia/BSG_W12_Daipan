using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
public class GetScene:MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR        
         if (Input.GetKeyUp(KeyCode.G))
         {
             Debug.Log("---- AssetDatabase ----");
             foreach (var guid in AssetDatabase.FindAssets("t:Scene"))
             {
                 var path = AssetDatabase.GUIDToAssetPath(guid);
                 Debug.Log(AssetDatabase.LoadMainAssetAtPath(path));
             }
         }
#endif         
    }
}
