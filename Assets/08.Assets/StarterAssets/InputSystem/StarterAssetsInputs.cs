using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
	public class StarterAssetsInputs : MonoBehaviour
	{
		public ThirdPersonController playerctrl;

		[Header("Character Input Values")]
		public Vector2 move;
		public Vector2 look;
		public bool jump;
		public bool sprint;
		public bool attack;
		public bool shield;
		public bool roll;

		[Header("Movement Settings")]
		public bool analogMovement;

		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;

        private void Start()
        {
            playerctrl = GetComponent<ThirdPersonController>();
        }

#if ENABLE_INPUT_SYSTEM
        public void OnMove(InputValue value)
		{
			MoveInput(value.Get<Vector2>());
		}

		public void OnLook(InputValue value)
		{
			if (playerctrl.isPopup) return;
			if(cursorInputForLook)
			{
				LookInput(value.Get<Vector2>());
			}
		}

		public void OnJump(InputValue value)
		{
			JumpInput(value.isPressed);
		}

		public void OnSprint(InputValue value)
		{
			SprintInput(value.isPressed);
		}

        public void OnAttack(InputValue value)
        {
            AttackInput(value.isPressed);
        }

		public void OnShield(InputValue value)
		{
			ShieldInput(value.isPressed);
		}

		public void OnRoll(InputValue value)
		{
			RollInput(value.isPressed);
		}
#endif


        public void MoveInput(Vector2 newMoveDirection)
		{
			move = newMoveDirection;
		} 

		public void LookInput(Vector2 newLookDirection)
		{
			look = newLookDirection;
		}

		public void JumpInput(bool newJumpState)
		{
			jump = newJumpState;
		}

		public void SprintInput(bool newSprintState)
		{
			sprint = newSprintState;
		}
        public void AttackInput(bool newAttackState)
        {
            if (playerctrl.isPopup) return;
            attack = newAttackState;
        }

		public void ShieldInput(bool newShieldState)
		{
			shield = newShieldState;
		}

		public void RollInput(bool newRollState)
		{
			roll = newRollState;
		}

        private void OnApplicationFocus(bool hasFocus)
		{
			SetCursorState(cursorLocked);
		}

		private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}
	}
	
}