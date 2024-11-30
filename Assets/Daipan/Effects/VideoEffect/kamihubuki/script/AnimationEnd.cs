using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Daipan.Enemy.MonoScripts;

public class AnimationEnd:MonoBehaviour
{
    [SerializeField]WaveTextMono anime=null!;
    public void AnimeEnd()
    {
        Debug.Log("AnimeEnd");
        anime.OnAnimationEnd();
    }
}
