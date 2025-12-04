using System;
using System.Collections.Generic;

public class ObjectPool<T>
{
    private const int MIN_PREWARM_COUNT = 0;

    private readonly Func<T> _createFunc;
    private readonly Action<T> _onGet;
    private readonly Action<T> _onRelease;
    
    private readonly Stack<T> _items;

    public ObjectPool(Func<T> createFunc, Action<T> onGet = null, Action<T> onRelease = null, int initialCapacity = 0)
    {
        _createFunc = createFunc ?? throw new ArgumentNullException(nameof(createFunc));
        _onGet = onGet;
        _onRelease = onRelease;
        _items = initialCapacity > 0 ? new Stack<T>(initialCapacity) : new Stack<T>();
    }

    public T Get()
    {
        T item = _items.Count > 0 ? _items.Pop() : _createFunc();

        _onGet?.Invoke(item);

        return item;
    }

    public void Release(T item)
    {
        if (item == null)
            return;

        _onRelease?.Invoke(item);
        
        _items.Push(item);
    }

    public void Prewarm(int count)
    {
        int safeCount = Math.Max(MIN_PREWARM_COUNT, count);

        for (int i = _items.Count; i < safeCount; i++)
        {
            T item = _createFunc();
            
            _onRelease?.Invoke(item);
            
            _items.Push(item);
        }
    }
}