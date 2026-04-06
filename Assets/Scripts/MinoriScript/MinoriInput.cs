using UnityEngine.InputSystem;
using UnityEngine;
using System;

public class MinoriInputs : MonoBehaviour
{
    public static MinoriInputs Instance { get; private set; }

    private PlayerInput playerInput;

    public event EventHandler OnMinoriAttack;
    public event EventHandler OnMinoriSkill;
    public event EventHandler OnMinoriUltimate;
    public event EventHandler OnMinoriAttackRelease;
    public event EventHandler OnMinoriCharChange;

    private DialogueManager dialogueManager;

    private void Awake()
    {
        Instance = this;

        playerInput = new PlayerInput();
        playerInput.Enable();

        playerInput.Attack.Attack.started += MinoriAttack_started;
        playerInput.Attack.Attack.canceled += MinoriAttack_canceled;
        playerInput.Attack.Skill.performed += MinoriHeal_performed;
        playerInput.Attack.Ultimate.performed += MinoriUltimate_performed;
        playerInput.Attack.CharacterChange.performed += MinoriCharChange_performed;

    }

    private void MinoriAttack_started(InputAction.CallbackContext obj)
    {
        OnMinoriAttack?.Invoke(this, EventArgs.Empty);
    }

    private void MinoriAttack_canceled(InputAction.CallbackContext obj)
    {
        OnMinoriAttackRelease?.Invoke(this, EventArgs.Empty);
    }

    public void MinoriHeal_performed(InputAction.CallbackContext obj)
    {
        OnMinoriSkill?.Invoke(this, EventArgs.Empty);
    }

    public void MinoriUltimate_performed(InputAction.CallbackContext obj)
    {
        OnMinoriUltimate?.Invoke(this, EventArgs.Empty);
    }

    public void MinoriCharChange_performed(InputAction.CallbackContext obj)
    {
        OnMinoriCharChange?.Invoke(this, EventArgs.Empty);
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
        Vector2 direction = (GetMousePosition() - Minori.Instance.GetPlayerScreenPosition()).normalized;
        return direction;
    }

    public Vector3 GetMouseWorldPosition(Camera camera)
    {
        Vector3 mousePos = Mouse.current.position.ReadValue();
        mousePos.z = camera.nearClipPlane;
        return camera.ScreenToWorldPoint(mousePos);
    }

}
