#nullable enable
using Daipan.Utility.Scripts;

namespace Daipan.Comment.Scripts
{
    public class CommentEnum : Enumeration
    {
        public static CommentEnum[] Values { get; }

        CommentEnum(int id, string name) : base(id, name)
        {
        }

        static CommentEnum()
        {
            Values = new[]
            {
                None,
                Normal,
                Super,
                Spiky,
            };
        }

        public static CommentEnum None = new(0, "None");
        public static CommentEnum Normal = new(1, "Normal");
        public static CommentEnum Super = new(2, "Super");
        public static CommentEnum Spiky = new(3, "Spiky");
        
    }
}