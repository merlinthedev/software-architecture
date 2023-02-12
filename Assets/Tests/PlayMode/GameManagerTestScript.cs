using System.Collections;
using NUnit.Framework;

using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.TestTools;

public class GameManagerTestScript {

    private GameManager gameManager;

    [OneTimeSetUp]
    public void loadScene() {
        SceneManager.LoadScene("SampleScene");
    }

    [UnityTest]
    public IEnumerator gameOverTest() {
        yield return new WaitForSeconds(2f);

        gameManager = GameManager.getInstance();

        yield return new WaitForEndOfFrame();

        gameManager.setGameOver(true);

        yield return new WaitForEndOfFrame();

        Assert.AreEqual(true, gameManager.isGameOver());


    }

    [UnityTest]
    public IEnumerator gameWonTest() {
        yield return new WaitForSeconds(2f);

        gameManager = GameManager.getInstance();

        yield return new WaitForEndOfFrame();

        gameManager.setGameWon(true);

        Assert.AreEqual(true, gameManager.isGameWon());
    }

}
