using UnityEngine;
using System.Collections;
public class RocketState : IState
{
    private readonly PropulsorManager _propulsorManager;
    private readonly InputManager _input;
    private readonly PlayerMovement _player;
    private readonly Animator _animator;

    private float _accRate = 3f;
    private float _sideThrustForce = 35f;
    private float _fuelRate = 3f;
    private float _energyRate = 0;

    public RocketState(PropulsorManager propulsorManager, InputManager input, PlayerMovement player, Animator animator)
    {
        _propulsorManager = propulsorManager;
        _input = input;
        _player = player;
        _animator = animator;
    }
    public void Tick()
    {
        if(_input.ForwardThrust())
        {
            _propulsorManager.Accelerate(_accRate, _fuelRate, _energyRate);
        }

        _animator.Play("RocketState");
        
    }

    public void OnEnter()
    {
        _propulsorManager.thrust.text = "Rocket";
        _player.sideThrustForce = _sideThrustForce;
        _player.energyRate = _energyRate;
        _player.fuelRate = _fuelRate;
        _animator.Play("RocketEnterState");
    }

    public void OnExit()
    {
        _animator.Play("RocketExitState");
    }
    private IEnumerator waitForAnimation(float animationLenght)
    {
        yield return new WaitForSeconds(animationLenght);
    }
}
