using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovingState : IState
{
    private PlayerController _player;

    public PlayerMovingState(PlayerController player)
    {
        _player = player;
    }

    public void Enter() { }

    public void HandleInput()
    {
        if (_player.MovementInput.magnitude == 0)
        {
            _player.StateMachine.ChangeState(_player.IdleState);
        }
    }
    public void FixedUpdate()
    {
        _player.RB.linearVelocity = _player.MovementInput * _player.moveSpeed;
    }
    public void Update()
    {
        
        if (_player.MovementInput != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(_player.MovementInput);
            _player.transform.rotation = Quaternion.Slerp(_player.transform.rotation, targetRotation, Time.deltaTime * 10f);
        }
    }
    

    public void Exit() { }
}
