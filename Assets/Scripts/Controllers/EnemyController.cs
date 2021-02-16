using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, IExecute
{
    private List<Enemy> _enemies = new List<Enemy>();
    // Start is called before the first frame update
    private void Start()
    {
        
    }

    public void Execute() 
    {
        foreach (var enemy in _enemies)
        {
            enemy.Execute();
        }
    }

    public void AddEnemy(Enemy enemy)
    {
        if (!_enemies.Contains(enemy))
        {
            _enemies.Add(enemy);
        }
    }

    public void RemoveEnemyFromList(Enemy enemy)
    {
        if (_enemies.Contains(enemy))
        {
            _enemies.Remove(enemy);
        }
    }
}
