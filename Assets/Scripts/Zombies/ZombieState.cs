using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ULTankZombies.Zombies;

namespace ULTankZombies.Zombies {

    public class ZombieState

    {
        protected ZombieController controller;
        protected ZombieStateMachine fsm;

        public ZombieState(ZombieController controller, ZombieStateMachine fsm)
        {
            this.controller = controller;
            this.fsm = fsm;
        }
        public virtual void onEnter() { }
        public virtual void onExit() { }
        public virtual void onHandleInput() { }
        public virtual void onPhysicsUpdate() { }
        public virtual void onLogicUpdate() { }
    }
}

