using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem.Controls;

public class HealthManager : AgentComponent
{
    public UnityEvent<double> OnHealthChanged;
    [SerializeField]
    private double health;
    public double Health => health;

    public void SetHealth(double health)
    {
        if (health < 0) throw new ArgumentException(nameof(HealthManager.health), "Health must be positive.");
        double difference = this.health - health;
        this.health = health;
        OnHealthChanged?.Invoke(difference);
    }

    public void ChangeHealth(double difference)
    {
        health += difference;
        if (health < 0) health = 0;
        OnHealthChanged?.Invoke(difference);
    }
}
