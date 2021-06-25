using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ULTankZombies.Tank
{
    public class AttackState : TankState
    {

        private float fireRate;
        private float specialFireRate;
        private float countdown;
        private float specialCountdown;
        // Start is called before the first frame update

        public AttackState(TankController controller, TankStateMachine fsm)
            : base(controller, fsm)
        {
            fireRate = controller.fireRate;
            specialFireRate = controller.specialFireRate;
            countdown= 0f;
            specialCountdown = 0f;
        }

        public override void OnEnter(bool shootType)
        {

            if (shootType && specialCountdown < Time.time)
            {
                controller.Fire(shootType);
                specialCountdown = Time.time + specialFireRate;
            }

            if (!shootType && countdown < Time.time)
            {
                controller.Fire(shootType);
                countdown = Time.time + fireRate;
            }
   
        }

        public override void OnHandleInput()
        {
            base.OnHandleInput();

            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            {
                controller.Move();
            }
            if (Input.GetAxis("Horizontal") == 0 || Input.GetAxis("Vertical") == 0)
            {
                controller.Stop();
            }
        }
        
    }
}