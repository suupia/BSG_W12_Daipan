#nullable enable
using System;
using UnityEngine;

namespace Daipan.Enemy.Interfaces;

public interface IEnemyViewAnimatorSwitcher
{
    public void SetHpGauge(double currentHp, int maxHp);

    public void Move();

    public void Attack();

    public void Died(Action onDied);

    public void Daipaned(Action onDied);

    public void Highlight(bool isHighlighted, SpriteRenderer HpSprite);

}