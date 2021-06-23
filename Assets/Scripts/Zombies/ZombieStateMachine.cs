using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ULTankZombies.Zombies {
    public class ZombieStateMachine
    {
        public ZombieState CurrentState { get; private set; }
        public void Start(ZombieState initialState)
        {
            CurrentState = initialState;
            CurrentState.onEnter();
        }

        public void ChangeState(ZombieState newstate) {
            CurrentState.onExit();
            CurrentState = newstate;
            CurrentState.onEnter();
        }
    }
}

