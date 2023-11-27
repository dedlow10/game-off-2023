using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] public string DisplayText;
    [SerializeField] protected float PowerUpLength;
    [SerializeField] protected GameObject Display;

    protected float currentPowerUpLength;

    public virtual void SetActivePowerUp(bool isActive) { }

    public float GetPowerUpLength()
    {
        return PowerUpLength; ;
    }
}
