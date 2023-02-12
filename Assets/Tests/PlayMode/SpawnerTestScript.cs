using System.Collections;
using NUnit.Framework;

using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.TestTools;

public class SpawnerTestScript {

    private EnemyManager enemyManager;

    [OneTimeSetUp]
    public void loadScene() {
        SceneManager.LoadScene("SampleScene");
    }

    [UnitySetUp]
    public IEnumerator setupTests() {
        yield return new WaitForSeconds(1f);

        enemyManager = GameObject.FindObjectOfType<EnemyManager>();
    }

    [UnityTest]
    public IEnumerator SpawnerSpawnsEnemyTest() {
        EnemyManager testManager = GameObject.Instantiate<EnemyManager>(enemyManager);

        yield return new WaitForEndOfFrame();


        yield return new WaitForSeconds(testManager.getSpawnRate() * 3f + testManager.getWaveDelay());

        Assert.AreEqual(3, testManager.getEnemyMap().Values.Count);

        
    }
    
}
