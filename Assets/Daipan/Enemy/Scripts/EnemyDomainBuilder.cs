using System.Linq;
using Daipan.Comment.Scripts;
using Daipan.Enemy.Interfaces;
using Daipan.Enemy.MonoScripts;
using Daipan.LevelDesign.Enemy.Scripts;
using Daipan.Stream.Scripts;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Daipan.Enemy.Scripts;

public class EnemyDomainBuilder : IEnemyDomainBuilder
{
    readonly CommentSpawner _commentSpawner;
    readonly EnemyParamsServer _enemyParamsServer;
    readonly ViewerNumber _viewerNumber;

    public EnemyDomainBuilder(
        CommentSpawner commentSpawner,
        ViewerNumber viewerNumber,
        EnemyParamsServer enemyParamsServer
    )
    {
        _commentSpawner = commentSpawner;
        _viewerNumber = viewerNumber;
        _enemyParamsServer = enemyParamsServer;
    }

    public EnemyMono SetDomain(EnemyMono enemyMono)
    {
        var enemyEnum = _enemyParamsServer.DecideRandomEnemyType();
        Debug.Log($"enemyEnum: {enemyEnum}");
        enemyMono.SetDomain(new EnemyAttack(enemyMono));
        enemyMono.SetParameter(enemyEnum);
        enemyMono.OnDied += (sender, args) =>
        {
            // ボスを倒したときも含む
            _enemyParamsServer.AddCurrentKillAmount();

            if (!args.IsBoss) _viewerNumber.IncreaseViewer(7);
            // if(args.IsBoss) _commentSpawner.SpawnCommentByType(CommentEnum.Super);
            // else _commentSpawner.SpawnCommentByType(CommentEnum.Normal);

            if (args.IsBoss || args.IsQuickDefeat) _commentSpawner.SpawnCommentByType(CommentEnum.Normal);
        };
        return enemyMono;
    }

    // 本来はScriptableObjectで制御するのでこれは後でパラメータをもらうようにして消す
    // 今はスクリプトで制御するために書いておく
    EnemyEnum DecideRandomEnemyTypeCustom()
    {
        var rand = Random.value;
        if (rand < 0.5f) return EnemyEnum.A;
        return EnemyEnum.Boss;
    }

    EnemyEnum DecideRandomFromAllEnemyType()
    {
        var enemyEnums = EnemyEnum.Values;
        var rand = Random.Range(0, enemyEnums.Count());
        return enemyEnums[rand];
    }
}