using Daipan.Comment.MonoScripts;
using Daipan.Comment.Scripts;
using Daipan.Core;
using Daipan.Core.Interfaces;
using Daipan.Daipan;
using Daipan.End.MonoScripts;
using Daipan.Enemy.Interfaces;
using Daipan.Enemy.MonoScripts;
using Daipan.Enemy.Scripts;
using Daipan.LevelDesign.Comment.Scripts;
using Daipan.LevelDesign.Enemy.Scripts;
using Daipan.Player.LevelDesign.Scripts;
using Daipan.LevelDesign.Tower.Scripts;
using Daipan.Player.Scripts;
using Daipan.Stream.MonoScripts;
using Daipan.Stream.Scripts;
using Daipan.Stream.Scripts.Utility;
using Daipan.Tower.Scripts;
using Daipan.Utility.Scripts;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Daipan.End
{
    public sealed class EndScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            DaipanScope.RegisterSound(builder);
            builder.RegisterComponentInHierarchy<EndMono>();

            DaipanScope.RegisterInputSerial(builder);
        }
    } 
}
