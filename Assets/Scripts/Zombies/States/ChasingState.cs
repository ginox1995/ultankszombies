using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ULTankZombies.Zombies {
    public class ChasingState : ZombieState
    {
        private Transform tank;
        private float speed;
        private float rotationSpeed;
        private Rigidbody rb;
        public ChasingState(ZombieController controller, ZombieStateMachine fsm) : base(controller, fsm)
        {
            tank = controller.tank;
            speed = controller.settings.speed;
            rotationSpeed = controller.settings.rotationSpeed;
            rb = controller.GetComponent<Rigidbody>();
        }

        public override void onPhysicsUpdate()
        {
            base.onPhysicsUpdate();
            Vector3 NewDirection = (controller.tank.position - controller.transform.position).normalized;

            Quaternion rotation = Quaternion.LookRotation(NewDirection);

            controller.transform.rotation = Quaternion.Slerp(
                controller.transform.rotation,
                rotation,
                rotationSpeed * Time.fixedDeltaTime
            );
            controller.transform.position = Vector3.MoveTowards(
                 controller.transform.position,
                 tank.position,
                 speed * Time.fixedDeltaTime
            );
        }
    }
}

