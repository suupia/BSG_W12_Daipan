#nullable enable
namespace Daipan.Comment.Scripts
{
    public struct CommentEnum
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public static CommentEnum[] Values { get; }

        CommentEnum(int id, string name)
        {
            (Id, Name) = (id, name);
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
        

        #region Overrides

        public static bool operator ==(CommentEnum a, CommentEnum b)
        {
            return a.Id == b.Id && a.Name == b.Name;
        }

        public static bool operator !=(CommentEnum a, CommentEnum b)
        {
            return !(a == b);
        }

        public override bool Equals(object? obj)
        {
            if (obj is CommentEnum enemyEnum) return this == enemyEnum;
            return false;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode() ^ Name.GetHashCode();
        }

        public override string ToString()
        {
            return Name;
        }

        #endregion
    }
}