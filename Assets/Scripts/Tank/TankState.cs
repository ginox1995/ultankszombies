using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ULTankZombies.Tank
{
    public abstract class TankState
    {
        protected TankController controller;
        protected TankStateMachine fsm;

        public TankState(TankController controller, TankStateMachine fsm)
        {
            this.controller = controller;
            this.fsm = fsm;
        }
        public virtual void OnEnter() { }
        public virtual void OnEnter(bool boolean) { }
        public virtual void OnExit() { }
        public virtual void OnHandleInput() { }
        public virtual void OnLogicUpdate() { }
        public virtual void OnPhysicsUpdate() { }
    }
}

