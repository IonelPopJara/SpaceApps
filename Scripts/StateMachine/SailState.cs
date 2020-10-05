using UnityEngine;

public class SailState : IState
{
    private readonly PropulsorManager _propulsorManager;
    private readonly InputManager _input;
    private readonly Animator _animator;
    private readonly PlayerMovement _player;

    private float _accRate = 0.1f;
    private float _fuelRate = 0f;
    private float _energyRate = -2f;
    private float _sideThrustForce = 5f;

    public SailState(PropulsorManager propulsorManager, InputManager input, PlayerMovement player, Animator animator)
    {
        _propulsorManager = propulsorManager;
        _input = input;
        _player = player;
        _animator = animator;
    }

    public void Tick()
    {
        _propulsorManager.thrust.text = "Sail";

        _propulsorManager.Accelerate(_accRate, _fuelRate, _energyRate);
        _animator.Play("SailState");
        _player.energyRate = _energyRate;
        _player.fuelRate = _fuelRate;
    }

    public void OnEnter()
    {
        _player.sideThrustForce = _sideThrustForce;
        _animator.Play("SailEnterState");
    }

    public void OnExit()
    {
        _animator.Play("SailExitState");
    }
}
