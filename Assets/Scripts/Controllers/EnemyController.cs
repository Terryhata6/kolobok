using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, IExecute
{
    private List<Enemy> _enemies = new List<Enemy>();
    private PlayerController _player;
    private Enemy _closestEnemy = null;
    private Enemy _oldClosestEnemy = null;
    private float _closestEnemyDistance = 0;
    private float _playerAttackDistance;
    private void Awake()
    {
        _player = FindObjectOfType<PlayerController>();
        _playerAttackDistance = _player.AttackDistance;
    }

    private void Start()
    {
        
    }

    public void Execute() 
    {
        foreach (var enemy in _enemies)
        {
            enemy.Execute();
            if (_closestEnemy == null)
            {
                _closestEnemy = enemy;
                _closestEnemyDistance = _closestEnemy.DistanceToPlayer;
            } else if (_closestEnemy.DistanceToPlayer > enemy.DistanceToPlayer)
            {
                _closestEnemy = enemy;
                _closestEnemyDistance = _closestEnemy.DistanceToPlayer;
            }
        }

        if (_oldClosestEnemy == null)
        {
            if ((_closestEnemy != null) && (_closestEnemyDistance < _playerAttackDistance))
            {
                _player.ChangePursuedObject(_closestEnemy);
                _oldClosestEnemy = _closestEnemy;
            }
            else
            {
                _player.SetPursedObjectNull();
            }
        }
        else if (_oldClosestEnemy != _closestEnemy)
        {
            _player.ChangePursuedObject(_closestEnemy);             
            _oldClosestEnemy = _closestEnemy;
        }

        if (_closestEnemy == null)
        {
            _closestEnemyDistance = _playerAttackDistance + 0.01f;
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
            if (_player.PursuedObject == enemy.gameObject)
            {
                _player.SetPursedObjectNull();
            }
        }
    }
}
