using UnityEngine.InputSystem;
using UnityEngine;
using System;

public class ReikoInputs : MonoBehaviour
{
    public static ReikoInputs Instance { get; private set; }

    private PlayerInput playerInput;

    public event EventHandler OnReikoAttack;
    public event EventHandler OnReikoDogde;
    public event EventHandler OnReikoUltimate;
    public event EventHandler OnReikoCharChange;

    private DialogueManager dialogueManager;

    private void Awake()
    {
        Instance = this;

        playerInput = new PlayerInput();
        playerInput.Enable();

        playerInput.Attack.Attack.started += ReikoAttack_started;
        playerInput.Attack.Skill.performed += ReikoSkill_performed;
        playerInput.Attack.Ultimate.performed += ReikoUltimate_performed;
        playerInput.Attack.CharacterChange.performed += ReikoCharChange_performed;
    }



    public void ReikoAttack_started(InputAction.CallbackContext obj)
    {
            OnReikoAttack?.Invoke(this, EventArgs.Empty);
        }


    public void ReikoSkill_performed(InputAction.CallbackContext obj)
    {
            OnReikoDogde?.Invoke(this, EventArgs.Empty);
    }

    public void ReikoUltimate_performed(InputAction.CallbackContext obj)
    {
        OnReikoUltimate?.Invoke(this, EventArgs.Empty);
    }

    public void ReikoCharChange_performed(InputAction.CallbackContext obj)
    {
        OnReikoCharChange?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVector()
    {
        Vector2 inputVector = playerInput.Walk.WASD.ReadValue<Vector2>();
        return inputVector;
    }

    public Vector3 GetMousePosition()
    {
        Vector3 mousePos = Mouse.current.position.ReadValue();
        return mousePos;
    }

    public Vector2 GetMouseDirection()
    {
        Vector2 direction = (GetMousePosition() - Reiko.Instance.GetPlayerScreenPosition()).normalized;
        return direction;
    }
}
