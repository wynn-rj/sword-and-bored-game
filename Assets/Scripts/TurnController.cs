using UnityEngine;

public class TurnController
{
    private readonly GameObject[] _entities;
    int pointer = 0;

    public TurnController(GameObject[] entities)
    {
        _entities = entities;
    }

    public TurnController(GameObject[] entities, IShuffler<GameObject> shuffler)
    : this(shuffler.shuffle(entities)) { }

    public GameObject nextEntity()
    {
        if(_entities.Length <= 0)
        {
            return null;
        }
        GameObject nextEntity = _entities[pointer];
        pointer += 1;
        pointer %= _entities.Length;

        return nextEntity;
    }

}
