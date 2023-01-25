public abstract class Event { }

public class EventBus<T> where T : Event {
    private static event System.Action<T> OnEventRaised;
    public static void Subscribe(System.Action<T> handler) {
        OnEventRaised += handler;
    }

    public static void Unsubscribe(System.Action<T> handler) {
        OnEventRaised -= handler;
    }

    public static void Raise(T e) {
        OnEventRaised?.Invoke(e);
    }
}

public class GlobalDamageEvent : Event {

    public Enemy enemy;

    public GlobalDamageEvent(Enemy enemy) {
        this.enemy = enemy;
    }

}

public class EnemyKilledEvent : Event {

    public Enemy enemy;

    public EnemyKilledEvent(Enemy enemy) {
        this.enemy = enemy;
    }

}

public class TowerPlacedEvent : Event {

    public Tower tower;

    public TowerPlacedEvent(Tower tower) {
        this.tower = tower;
    }

}

public class TowerSelectedEvent : Event {

    public Tower tower;

    public TowerSelectedEvent(Tower tower) {
        this.tower = tower;
    }

}

public class TowerUnselectEvent : Event {

    public bool isUnselected;

    public TowerUnselectEvent(bool isUnselected) {
        this.isUnselected = isUnselected;
    }

}

public class TowerUpgradeEvent: Event {

    public string upgradeType;
    public Tower tower;

    public TowerUpgradeEvent(string upgradeType, Tower tower) {
        this.upgradeType = upgradeType;
        this.tower = tower;
    }

}

public class UpdateHealthEvent : Event {

    public int health;

    public UpdateHealthEvent(int health) {
        this.health = health;
    }
}

public class RemoveMoneyEvent : Event {

    public int value;

    public RemoveMoneyEvent(int value) {
        this.value = value;
    }

}

public class UpdateMoneyEvent : Event {

    public int money;

    public UpdateMoneyEvent(int money) {
        this.money = money;
    }
}

public class UpdateWaveEvent : Event {

    public int wave;

    public UpdateWaveEvent(int wave) {
        this.wave = wave;
    }
}

public class GameIsWonEvent : Event {

    public bool isWon;

    public GameIsWonEvent(bool isWon) {
        this.isWon = isWon;
    }

}

public class GameIsOverEvent : Event {

    public bool isGameOver;

    public GameIsOverEvent(bool isGameOver) {
        this.isGameOver = isGameOver;
    }

}

public class WavePauseEvent : Event {

    public bool isPaused;
    public int timeLeft;

    public WavePauseEvent(bool isPaused, int timeLeft) {
        this.isPaused = isPaused;
        this.timeLeft = timeLeft;
    }

}


