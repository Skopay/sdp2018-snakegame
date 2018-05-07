using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class FoodTest
{
    [UnityTest]
    public IEnumerator FoodTester()
    {

        //This will create a new game object of the main game controller so methods can be called for testing
        var gameObject = new GameController();

        //This will create a temp size variable of the Snake when it is initially spawned. The "Hit()" method is then called which should then
        //increase the size of the Snake. This is tested, and the test will pass if the above is true.
        int size = gameObject.maxSize;
        gameObject.Hit("Food");
        bool sizeIncrease = false;
        if (gameObject.maxSize > size)
        {
            sizeIncrease = true;
        }
        Assert.IsTrue(sizeIncrease);

        yield return null;
    }
}
