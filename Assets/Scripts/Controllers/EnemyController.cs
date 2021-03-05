using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : IExecute, IInitialize
{
    private List<Enemy> _enemies = new List<Enemy>();
    private PlayerController _player;
    private Enemy _closestEnemy = null;
    private Enemy _oldClosestEnemy = null;
    private float _closestEnemyDistance = 0;
    private float _playerAttackDistance;

    #region IInitialize
    public void Initialize() 
    {
        _playerAttackDistance = _player.AttackDistance; 
    
    }
    #endregion
    #region IExecute
    public void Execute() 
    {
        #region Execute
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
        #endregion

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
    #endregion
    #region Methods
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

    public void SetPlayer(PlayerController player)
    {
        _player = player;
    }
    #endregion
}
