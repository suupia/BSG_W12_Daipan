#nullable enable

using Daipan.Comment.Scripts;

namespace Daipan.Comment.Interfaces
{
    public interface ICommentSpawner
    {
        public void SpawnCommentByType(CommentEnum commentEnum);
    }
}