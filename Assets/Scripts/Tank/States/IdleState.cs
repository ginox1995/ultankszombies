using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ULTanksZombies.Tank
{
    public class IdleState : TankState
    {

        private Rigidbody rb;

        public IdleState(TankController controller, TankStateMachine fsm)
            : base(controller, fsm)
        {
            rb = controller.GetComponent<Rigidbody>();
            /*speed = controller.speed;
            rotationSpeed = controller.rotationSpeed;
            fireRate = controller.fireRate;
            specialFireRate = controller.specialFireRate;
            audioSource = controller.GetComponent<AudioSource>();
             */
        }
        public override void OnHandleInput()
        {
            base.OnHandleInput();

            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            {
                //Debug.Log("move");
                controller.Move();
            }
        }
    }

}
