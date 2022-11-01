using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class TestSuite
    {

        private Game game;

        // A Test behaves as an ordinary method
        [Test]
        public void TestSuiteSimplePasses()
        {
            // Use the Assert class to test conditions
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator TestSuiteWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }



        [UnityTest]
        public IEnumerator AsteroidsMoveDown()
        {
            GameObject gameGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Game"));
            game = gameGameObject.GetComponent<Game>();

            GameObject asteroid = game.GetSpawner().SpawnAsteroid();

            float initialYPos = asteroid.transform.position.y;


            yield return new WaitForSeconds(0.1f);

            Assert.Less(asteroid.transform.position.y, initialYPos);

            Object.Destroy(game.gameObject);
        }
    }
}
