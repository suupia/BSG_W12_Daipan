#nullable enable
using System;

namespace Daipan.Enemy.Interfaces;

public interface IEnemySpecialView
{
    void SetSpecialBlackCallback(Action onSpecialBlack);
    void SpecialBlack();
}