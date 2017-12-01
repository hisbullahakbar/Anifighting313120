using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyStateFSM
{
    void Enter(Enemy enemy);
    void Execute();
    void Exit();
    void OnTriggerEnter(Collider2D other);
    string getStateName();
}