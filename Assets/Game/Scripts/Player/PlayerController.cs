using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;

    public Rigidbody RB { get; private set; }
    public Vector3 MovementInput { get; private set; }

    public PlayerStateMachine StateMachine { get; private set; }

    public PlayerIdleState IdleState { get; private set; }

    public PlayerMovingState MovingState { get; private set; }

    private PlayerShooter _shooter;

    private void Awake()
    {
        RB = GetComponent<Rigidbody>();

        
    }
    [Zenject.Inject]
    public void Construct(PlayerIdleState idleState, PlayerMovingState movingState, PlayerShooter shooter)
    {
        IdleState = idleState;
        MovingState = movingState;
        _shooter = shooter;


        StateMachine = new PlayerStateMachine();
        StateMachine.Initialize(IdleState);
    }

    private void Update()
    {
        HandleInput();
        if (StateMachine != null && StateMachine.CurrentState != null)
        {
            StateMachine.CurrentState.HandleInput();
            StateMachine.CurrentState.Update();
        }

        if (Input.GetMouseButton(0))
        {
            _shooter.TryShoot();
        }
    }

    private void FixedUpdate()
    {
        if (StateMachine != null && StateMachine.CurrentState != null)
        {
            StateMachine.CurrentState.FixedUpdate();
        }
    }

    private void HandleInput()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");
        MovementInput = new Vector3(-moveZ, 0f, moveX).normalized;
    }
}
