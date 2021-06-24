using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ULTankZombies.Tank
{
    public class IdleState : TankState
    {
        
        private float fireRate;
        private float specialFireRate;
        private bool shoot = false;
        private float countdown = 0f;
        private bool specialShoot = false;
        private float specialCountdown = 0f;

        public IdleState(TankController controller, TankStateMachine fsm)
            : base(controller, fsm)
        {
            
            fireRate = controller.fireRate;
            specialFireRate = controller.specialFireRate;
        }
        public override void OnHandleInput()
        {
            base.OnHandleInput();

            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            {
                //Debug.Log("move");
                controller.Move();
            }

            if (Input.GetMouseButtonDown(0) && countdown <= 0f)
            {
                //Fire
                shoot = true;
                countdown = 1f / fireRate;
            }
            countdown -= Time.deltaTime;

            if (Input.GetMouseButtonDown(1) && specialCountdown <= 0f)
            {
                //Fire

                specialShoot = true;
                specialCountdown = 1f / specialFireRate;
            }
            specialCountdown -= Time.deltaTime;
        }

        public override void OnLogicUpdate()
        {
            base.OnLogicUpdate();
            if (shoot)
            {
                controller.Fire(false);
                shoot = false;
            }
            if (specialShoot)
            {
                controller.Fire(true);
                specialShoot = false;
            }

        }
    }

    }

