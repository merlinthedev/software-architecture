using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class FirstTestScript
{
    [UnityTest]
    public IEnumerator CheckForBuildingPhase()
    {
        var gameObject = new GameObject();
        var manager = gameObject.AddComponent<GameManager>();

        Assert.AreEqual(true, manager.isBuildingPhase());
        yield return null;
    }

    [UnityTest]
    public IEnumerator ShouldSpawnCheck() {

        var gameObject = new GameObject();
        var manager = gameObject.AddComponent<EnemyManager>();

        Assert.AreEqual(true, manager.getShouldSpawn());

        yield return null;
    }

}
