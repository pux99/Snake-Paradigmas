using Game;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UnitTest
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void CheckCollisionUnitTest()
        {
            float rect1X = 10;
            float rect1Y = 10;
            float rect1Width = 10;
            float rect1Height = 10;

            float rect2X = 15;
            float rect2Y = 15;
            float rect2Width = 5;
            float rect2Height = 5;

            var resultado = false;
            var resultadoEsperado = Collision.RectRect(rect1X, rect1Y, rect1Width, rect1Height, rect2X, rect2Y, rect2Width, rect2Height);

            if (Collision.RectRect(rect1X, rect1Y, rect1Width, rect1Height, rect2X, rect2Y, rect2Width, rect2Height))
            {
                resultado = true;
            }

            Assert.AreEqual(resultado, resultadoEsperado);
        }

        [TestMethod]
        public void LifeChangeUnitTest()
        {
            float lossLife = 2;
            float totalLife = GameManager.Instance.lives;

            float resultadoEsperado = totalLife - lossLife;

            for (int i = 0; i < lossLife; i++)
            {
                GameManager.Instance.lives--;
            }

            var resultado = GameManager.Instance.lives;

            Assert.AreEqual(resultado, resultadoEsperado);
        }
        [TestMethod]
        public void ContadorManzana()
        {
            float resultadoEsperado = 1;

            Transform snackHead = new Transform(0, 0, 0, 1, 1);
            Transform Fruit = new Transform(0, 0, 0, 1, 1);

            if (Collision.RectRect(snackHead.position.x, snackHead.position.y, snackHead.scale.x, snackHead.scale.y,
                Fruit.position.x, Fruit.position.y, Fruit.scale.x, Fruit.scale.y))
            {
                GameManager.Instance.points++;
            }

            float resultado = GameManager.Instance.points;

            Assert.AreEqual(resultado, resultadoEsperado);
        }
    }
}
