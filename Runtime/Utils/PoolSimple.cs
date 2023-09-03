using System;
using System.Collections.Generic;

public class PoolSimple<T>
{
    private Queue<T> _items = new Queue<T>();
    private Func<T> _spawnItemsAction;

    public PoolSimple(int numItems, Func<T> spawnItem) {
        _spawnItemsAction = spawnItem;
        SpawnItems(numItems);
    }

    private void SpawnItems(int numItems) {
        for(int i = 0; i < numItems; ++i) {
            _items.Enqueue(_spawnItemsAction.Invoke());
        }
    }

    public T Get() {
        if(_items.Count <= 0) {
            SpawnItems(1);
        }

        var item = _items.Dequeue();
        return item;
    }

    public void Add(T item) {
        _items.Enqueue(item);
    }
}
