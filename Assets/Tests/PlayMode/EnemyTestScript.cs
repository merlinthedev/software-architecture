using System.Collections;
using NUnit.Framework;

using System.Linq;

using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.TestTools;

public class EnemyTestScript {

    [OneTimeSetUp]
    public void LoadScene() {
        SceneManager.LoadScene("SampleScene");
    }


    [UnityTest]
    public IEnumerator enemyDamageTest() {
        float damage = 10;
        yield return new WaitForSeconds(7f);

        Enemy testEnemy = EnemyManager.getInstance().getEnemyMap().ElementAt(0).Value;
        yield return new WaitForEndOfFrame();

        testEnemy.takeDamage(damage, Enemy.DamageType.FLAT);

        Assert.AreEqual(testEnemy.MaxHealth - damage, testEnemy.Health);

    }

    [UnityTest]
    public IEnumerator enemyNegativeHPtest() {
        yield return new WaitForSeconds(7f);
        Enemy testEnemy = EnemyManager.getInstance().getEnemyMap().ElementAt(0).Value;
        yield return new WaitForEndOfFrame();


        testEnemy.takeDamage(testEnemy.Health + 1, Enemy.DamageType.FLAT);
        Assert.AreEqual(0, testEnemy.Health);

    }

}
