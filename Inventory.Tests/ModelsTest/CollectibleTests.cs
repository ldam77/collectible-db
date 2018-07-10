using Microsoft.VisualStudio.TestTools.UnitTesting;
using Inventory.Models;
using System;
using System.Collections.Generic;

namespace Inventory.Tests
{
  [TestClass]
  public class CollectibleTests : IDisposable
  {
    public void Dispose()
    {
      Collectible.DeleteAll();
    }
    public CollectibleTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=inventory_test;";
    }
    [TestMethod]
    public void GetAll_DbStartsEmpty_0()
    {
      //Arrange
      //Act
      int result = Collectible.GetAll().Count;

      //Assert
      Assert.AreEqual(0, result);
    }
    [TestMethod]
    public void Equals_ReturnsTrueIfNameAndDescriptionsAreTheSame_Collectible()
    {
      // Arrange, Act
      Collectible firstCollectible = new Collectible("testOne","One description");
      Collectible secondCollectible = new Collectible("testOne","One description");

      // Assert
      Assert.AreEqual(firstCollectible, secondCollectible);
    }
    [TestMethod]
    public void Save_SavesToDatabase_ItemList()
    {
      //Arrange
      Collectible testCollectible = new Collectible("testOne","One description");

      //Act
      testCollectible.Save();
      List<Collectible> result = Collectible.GetAll();
      List<Collectible> testList = new List<Collectible>{testCollectible};
      Console.WriteLine(result[0].GetId());
      //Assert
      CollectionAssert.AreEqual(testList, result);
    }
  }
}
