using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class SampleTest
    {
        // A Test behaves as an ordinary method
        [Test]
        public void SampleTestSimplePasses()
        {
            // Use the Assert class to test conditions
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator JoystickMoveRight()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            GameObject player = new GameObject();
            var pBody = player.AddComponent<Rigidbody2D>();
            var pAva = player.AddComponent<PlayerAvatar>();
            var pMove = player.AddComponent<PlayerMovement>();
            pBody.velocity = Vector3.zero;

            yield return null;

            Vector3 leftJS = new Vector3(1,0,0);
            pAva.change = leftJS;
            pMove.speed = 10;

            yield return new WaitForFixedUpdate();

            Assert.IsTrue(pBody.velocity.x > 0);
        }
        [UnityTest]
        public IEnumerator JoystickMoveLeft()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            GameObject player = new GameObject();
            var pBody = player.AddComponent<Rigidbody2D>();
            var pAva = player.AddComponent<PlayerAvatar>();
            var pMove = player.AddComponent<PlayerMovement>();
            pBody.velocity = Vector3.zero;

            yield return null;

            Vector3 leftJS = new Vector3(-1,0,0);
            pAva.change = leftJS;
            pMove.speed = 10;

            yield return new WaitForFixedUpdate();

            Assert.IsTrue(pBody.velocity.x < 0);
        }
        [UnityTest]
        public IEnumerator JoystickMoveUp()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            GameObject player = new GameObject();
            var pBody = player.AddComponent<Rigidbody2D>();
            var pAva = player.AddComponent<PlayerAvatar>();
            var pMove = player.AddComponent<PlayerMovement>();
            pBody.velocity = Vector3.zero;

            yield return null;

            Vector3 leftJS = new Vector3(0,1,0);
            pAva.change = leftJS;
            pMove.speed = 10;

            yield return new WaitForFixedUpdate();

            Assert.IsTrue(pBody.velocity.y > 0);
        }
        [UnityTest]
        public IEnumerator JoystickMoveDown()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            GameObject player = new GameObject();
            var pBody = player.AddComponent<Rigidbody2D>();
            var pAva = player.AddComponent<PlayerAvatar>();
            var pMove = player.AddComponent<PlayerMovement>();
            pBody.velocity = Vector3.zero;

            yield return null;

            Vector3 leftJS = new Vector3(0,-1,0);
            pAva.change = leftJS;
            pMove.speed = 10;

            yield return new WaitForFixedUpdate();

            Assert.IsTrue(pBody.velocity.y < 0);
        }
    }
}
