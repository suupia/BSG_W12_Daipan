#nullable enable
using Daipan.Utility.Scripts;
using NUnit.Framework;


public class EnumEnumerationCheckerTest
{
    [Test]
    public void CheckEnumType1()
    {
        Assert.IsTrue(EnumEnumerationChecker.CheckEnum<EnumType1, TestEnum1>());
    }

    enum EnumType1
    {
        A,
        B
    }

    class TestEnum1 : Enumeration
    {
        public static TestEnum1[] Values { get; }

        TestEnum1(int id, string name) : base(id, name)
        {
        }

        static TestEnum1()
        {
            Values = new[]
            {
                A, B
            };
        }

        public static TestEnum1 A = new(0, "A");
        public static TestEnum1 B = new(1, "B");
    }

    [Test]
    public void CheckEnumType2()
    {
        Assert.IsFalse(EnumEnumerationChecker.CheckEnum<EnumType2, TestEnum2>());
    }

    enum EnumType2
    {
        A,
        B,
        C
    }

    class TestEnum2 : Enumeration
    {
        public static TestEnum2[] Values { get; }

        TestEnum2(int id, string name) : base(id, name)
        {
        }

        static TestEnum2()
        {
            Values = new[]
            {
                A, B
            };
        }

        public static TestEnum2 A = new(0, "A");
        public static TestEnum2 B = new(1, "B");
    }
}