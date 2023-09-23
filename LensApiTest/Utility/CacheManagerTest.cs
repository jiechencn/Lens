using Me.JieChen.Lens.Api.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.JieChen.Lens.Api.Utility;

[TestClass]
public class CacheManagerTest
{
    [TestMethod]
    public void GetInstance_TwoInstances_Should_Be_Same()
    {
        // Act
        CacheManager cacheManager1 = CacheManager.GetInstance();
        CacheManager cacheManager2 = CacheManager.GetInstance();

        // Assert
        Assert.AreEqual(cacheManager1.GetHashCode(), cacheManager2.GetHashCode());
    }

    [TestMethod]
    public void GetAsync_Twice_Should_Be_Same()
    {
        // Arrange
        string key = "mykey";
        Mock<AbstractFoo> mockFoo = new Mock<AbstractFoo>();

        // Act
        var result1 = CacheManager.GetInstance().GetAsync<string, Student>(key, () => ReadFromExternalDataSourceAsync(key, mockFoo.Object)).Result;
        // Assert
        mockFoo.Verify(f => f.Do(), Times.Once);

        // Arrange
        mockFoo.Invocations.Clear();
        // Act
        var result2 = CacheManager.GetInstance().GetAsync<string, Student>(key, () => ReadFromExternalDataSourceAsync(key, mockFoo.Object)).Result;
        // Assert
        mockFoo.Verify(f => f.Do(), Times.Never);

        // Assert
        Assert.AreEqual(result1, result2);
    }

    private async Task<Student> ReadFromExternalDataSourceAsync(string key, AbstractFoo foo)
    {
        foo.Do();
        return await Task.FromResult(new Student { Name = "foo", Age = 20 }).ConfigureAwait(false);
    }

    private class Student
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}

public abstract class AbstractFoo
{
    public AbstractFoo()
    {

    }
    public virtual void Do()
    {

    }
}

