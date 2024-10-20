#nullable enable
using Daipan.Utility.Scripts;
using NUnit.Framework;


public class EnumEnumerationCheckerTest
{
    [Test]
    public void CheckEnumType1()
    {
        Assert.IsTrue(EnumEnumerationChecker.CheckEnum<EnumType1, TestEnum>());
    }

    [Test]
    public void CheckEnumType2()
    {
        Assert.IsFalse(EnumEnumerationChecker.CheckEnum<EnumType2, TestEnum>());
    }
    
    [Test]
    public void CheckEnumType3()
    {
        Assert.IsFalse(EnumEnumerationChecker.CheckEnum<EnumType3, TestEnum>());
    }
    
    [Test]
    public void CheckEnumType4()
    {
        Assert.IsFalse(EnumEnumerationChecker.CheckEnum<EnumType4, TestEnum>());
    }
    enum EnumType1
    {
        A,
        B
    }

    enum EnumType2
    {
        A,
        B,
        C
    }

    enum EnumType3
    {
        A
    }

    enum EnumType4
    {
        X,Y
    }
    
    class TestEnum : Enumeration
    {
        public static TestEnum[] Values { get; }

        TestEnum(int id, string name) : base(id, name)
        {
        }

        static TestEnum()
        {
            Values = new[]
            {
                A, B
            };
        }

        public static TestEnum A = new(0, "A");
        public static TestEnum B = new(1, "B");
    }
}