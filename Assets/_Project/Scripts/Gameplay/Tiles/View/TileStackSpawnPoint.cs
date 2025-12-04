using UnityEngine;
using Zenject;

public class TileStackSpawnPoint : MonoBehaviour
{
    private const bool SPAWN_ON_START = true;

    private TileStackFactory _tileStackFactory;
    private TileStackRoot _currentStackRoot;

    [Inject]
    public void Construct(TileStackFactory tileStackFactory)
    {
        _tileStackFactory = tileStackFactory;
    }

    private void Start()
    {
        if (SPAWN_ON_START)
            TrySpawnStack();
    }
    
    public void NotifyStackPlaced(TileStackDraggable draggable)
    {
        if (_currentStackRoot == null)
            return;

        TileStackDraggable currentDraggable = _currentStackRoot.Draggable;

        if (currentDraggable == null || currentDraggable != draggable)
            return;

        _currentStackRoot = null;
        
        TrySpawnStack();
    }

    private void TrySpawnStack()
    {
        if (_tileStackFactory == null)
            return;
        
        if (_currentStackRoot != null && _currentStackRoot.gameObject.activeSelf)
            return;

        _currentStackRoot = _tileStackFactory.CreateOrReuseSpawnStack(transform);

        if (_currentStackRoot == null)
            return;

        TileStackDraggable draggable = _currentStackRoot.Draggable;

        if (draggable != null)
            draggable.Initialize(this);
    }
}