using Me.JieChen.Lens.Extensions;

namespace Test.JieChen.Lens.Extensions;

[TestClass]
public class EnumExtensionsTest
{

    [TestMethod]
    public void ToDescription_IfNoDescription_Should_ReturnItself()
    {
        // Act
        var desc = EnumExtensions.ToDescription(TestEnum.Fail);

        // Assert
        Assert.AreEqual(nameof(TestEnum.Fail), desc);
    }

    [TestMethod]
    public void ToDescription_IfHasDescription_Should_ReturnDescription()
    {
        // Act
        var desc = EnumExtensions.ToDescription(TestEnum.Success);

        // Assert
        Assert.AreEqual("OK", desc);
    }

    private enum TestEnum
    {
        [System.ComponentModel.Description("OK")]
        Success,
        Fail
    }
}
