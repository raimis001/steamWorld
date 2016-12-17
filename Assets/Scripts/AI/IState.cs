using UnityEngine;
using System.Collections;

public interface IState
{
    void OnStateEnter();
    void Update(StateMessager messager);
    void OnStateExit();
    string GetName();
}
