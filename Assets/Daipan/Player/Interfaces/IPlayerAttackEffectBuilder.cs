#nullable enable
using System;
using System.Collections.Generic;
using Daipan.Core.Interfaces;
using Daipan.Player.MonoScripts;
using JetBrains.Annotations;

namespace Daipan.Player.Interfaces
{
    public interface IPlayerAttackEffectBuilder
    {
        [MustUseReturnValue]
        Func<IPlayerAttackEffectMono, IPlayerAttackEffectMono> Build(IMonoBehaviour playerMono, List<AbstractPlayerViewMono?> playerViewMonos, PlayerColor playerColor);
    } 
}

