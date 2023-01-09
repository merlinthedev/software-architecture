public abstract class Event { }

public class EventBus<T> where T : Event {
    public static event System.Action<T> OnEventRaised;
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

public class UpdateHealthEvent : Event {

    public int health;

    public UpdateHealthEvent(int health) {
        this.health = health;
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