using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ULTankZombies.Tank
{
    public class IdleState : TankState
    {
                
        public IdleState(TankController controller, TankStateMachine fsm)
            : base(controller, fsm)
        {
            
        }
        public override void OnHandleInput()
        {
            base.OnHandleInput();

            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            {
                controller.Move();
            }
            
            if (Input.GetMouseButtonDown(0))
            {                
                controller.Attack(false);   
            }
            

            if (Input.GetMouseButtonDown(1))
            {
                controller.Attack(true);
            }
            
        }

      }

    }

