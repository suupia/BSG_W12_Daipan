#nullable enable
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAntiCommentParam
{
    public int MaxCharacterNumber { get; }
    public int BanCount { get; }
    public int SpecialAntiCommentHp { get; }
}
