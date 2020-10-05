using UnityEngine;
public class IonState : IState
{
    private readonly PropulsorManager _propulsorManager;
    private readonly InputManager _input;
    private readonly PlayerMovement _player;
    private readonly Animator _animator;

    private float _accRate = 0.1f;
    private float _fuelRate = 0f;
    private float _energyRate = 1f;
    private float _sideThrustForce = 20f;

    public IonState(PropulsorManager propulsorManager, InputManager input, PlayerMovement player, Animator animator)
    {
        _propulsorManager = propulsorManager;
        _input = input;
        _player = player;
        _animator = animator;
    }

    public void Tick()
    {
        _propulsorManager.thrust.text = "Ion";
        if(_input.ForwardThrust())
        {
            _propulsorManager.Accelerate(_accRate, _fuelRate, _energyRate);
        }
        _animator.Play("IonState");
    }

    public void OnEnter()
    {
        _player.sideThrustForce = _sideThrustForce;
        _animator.Play("IonEnterState");
        _player.energyRate = _energyRate;
        _player.fuelRate = _fuelRate;
    }

    public void OnExit()
    {
        _animator.Play("IonExitState");
    }
}
