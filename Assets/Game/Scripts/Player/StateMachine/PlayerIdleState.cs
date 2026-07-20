using Unity.VisualScripting;
using UnityEngine;

public class PlayerIdleState : IState
{
    private PlayerController _player;

    public PlayerIdleState(PlayerController player)
    {
        _player = player;
    }
    public void Enter() { }

    public void HandleInput()
    {
        if (_player.MovementInput.magnitude > 0)
        {
            _player.StateMachine.ChangeState(_player.MovingState);
        }
    }

    public void Update()
    {
        
    }

    public void FixedUpdate()
    {
        _player.RB.linearVelocity = Vector3.zero; // Fizik işlemi burada
    }
    public void Exit() { }
    }
