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

        


        [SetUp]
        public void Setup()
        {
            GameObject gameGameObject =
                MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Game"));
            game = gameGameObject.GetComponent<Game>();
        }

        [TearDown]
        public void Teardown()
        {
            Object.Destroy(game.gameObject);
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


        [UnityTest]
        public IEnumerator GameOverOccursOnAsteroidCollision()
        {
            GameObject gameGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Game"));
            game = gameGameObject.GetComponent<Game>();

            GameObject asteroid = game.GetSpawner().SpawnAsteroid();

            asteroid.transform.position = game.GetShip().transform.position;


            yield return new WaitForSeconds(0.1f);

            Assert.True(game.isGameOver);

            Object.Destroy(game.gameObject);
        }

        [UnityTest]
        public IEnumerator NewGameRestartsGame()
        {
            
            game.isGameOver = true;
            game.NewGame();
            
            Assert.False(game.isGameOver);
            yield return null;
        }

        [UnityTest]
        public IEnumerator LaserMovesUp()
        {
            
            GameObject laser = game.GetShip().SpawnLaser();
            
            float initialYPos = laser.transform.position.y;
            yield return new WaitForSeconds(0.1f);
            
            Assert.Greater(laser.transform.position.y, initialYPos);
        }

        [UnityTest]
        public IEnumerator LaserDestroysAsteroid()
        {
           
            GameObject asteroid = game.GetSpawner().SpawnAsteroid();

            asteroid.transform.position = Vector3.zero;

            GameObject laser = game.GetShip().SpawnLaser();

            laser.transform.position = Vector3.zero;

            yield return new WaitForSeconds(0.1f);
            
            UnityEngine.Assertions.Assert.IsNull(asteroid);
        }


        [UnityTest]
        public IEnumerator DestroyedAsteroidRaisesScore()
        {
            
            GameObject asteroid = game.GetSpawner().SpawnAsteroid();

            asteroid.transform.position = Vector3.zero;

            GameObject laser = game.GetShip().SpawnLaser();

            laser.transform.position = Vector3.zero;

            yield return new WaitForSeconds(0.1f);

            
            Assert.AreEqual(game.score, 1);
        }



    }
}
