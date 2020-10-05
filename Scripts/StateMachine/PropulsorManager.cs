using Cinemachine;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class PropulsorManager : MonoBehaviour
{
    [Header("References")]
    public PlayerMovement player;
    public CinemachineDollyCart dollyCart;
    public Animator animator;
    public GameObject button2;
    public GameObject button3;
    public GameObject warpParticles;

    public TextMeshProUGUI thrust;
    public TextMeshProUGUI speed;
    public TextMeshProUGUI fuel;
    public TextMeshProUGUI energy;

    [Space]

    [Header("Current Resources")]
    public float currentFuel;
    public float currentEnergy;
    public float currentSpeed = 0f;

    [Header("System Upgrades")]
    public bool rocketEnabled = true;
    public bool ionEnabled = false;
    public bool sailEnabled = false;
    public bool warpEnabled = false;

    private StateMachine _stateMachine;


    private float maxFuel = 100f;
    private float maxEnergy = 100f;

    private void Awake()
    {
        _stateMachine = new StateMachine();

        var rocket = new RocketState(this, player.input, player, animator);
        var ion = new IonState(this, player.input, player, animator);
        var sail = new SailState(this,player.input, player, animator);

        _stateMachine.SetState(rocket);

        //Activar estados cuando esten disponibles
        //void At(IState from, IState to, Func<bool> condition) => _stateMachine.AddTransition(from, to, condition);

        _stateMachine.AddAnyTransition(ion, TransitionToIon());
        _stateMachine.AddAnyTransition(rocket,TransitionToRocket());
        _stateMachine.AddAnyTransition(sail, TransitionToSail());

        Func<bool> TransitionToRocket() => () => (player.input.Num1());
        Func<bool> TransitionToIon() => () => (player.input.Num2() && ionEnabled);
        Func<bool> TransitionToSail() => () => (player.input.Num3() && sailEnabled);

        currentEnergy = maxEnergy;
        currentFuel = maxFuel;
    }

    private void Update()
    {
        _stateMachine.Tick();
        dollyCart.m_Speed = currentSpeed;
        ClampResources();
        Debug.Log("State: " + _stateMachine._currentState);

        speed.text = "Speed: " + currentSpeed.ToString("F2");

        fuel.text = "Fuel: " + currentFuel.ToString("0") + "%";
        energy.text = "Energy: " + currentEnergy.ToString("0") + "%";

        if(ionEnabled) button2.SetActive(true);
        if(sailEnabled) button3.SetActive(true);

        if(currentSpeed > 500) warpParticles.SetActive(true);
        else
        {
            warpParticles.SetActive(false);
        }
    }

    public void Accelerate(float accRate, float fuelRate, float energyRate)
    {
        //When accelerating with a certain system it lowers the resources. If the sail is equiped then it increments the energy.
        Debug.Log("Accelerate & lower resources");

        currentSpeed += accRate * Time.deltaTime;
        currentFuel -= fuelRate * Time.deltaTime;
        currentEnergy -= energyRate * Time.deltaTime;
    }

    public void AddEnergy(float incrementAmmount)
    {
        //Select a resource and adds an amount. This function is used for getting items when destroying asteroids. You can use negative amounts to lower resources.
        currentEnergy += incrementAmmount;
    }

    public void AddFuel(float incrementAmmount)
    {
        //Select a resource and adds an amount. This function is used for getting items when destroying asteroids. You can use negative amounts to lower resources.
        currentFuel += incrementAmmount;
    }

    private void ClampResources()
    {
        currentFuel = Mathf.Clamp(currentFuel, 0f, maxFuel);
        currentEnergy = Mathf.Clamp(currentEnergy, 0f, maxEnergy);
    }

    public void EnableUpgrade(int upgrade)
    {
        if(upgrade == 1) ionEnabled = true;
        else if(upgrade == 2) sailEnabled = true;
        else if(upgrade == 3) warpEnabled = true;

    }
}
