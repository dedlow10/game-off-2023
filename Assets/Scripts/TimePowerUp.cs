using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimePowerUp : PowerUp
{
    [SerializeField] float time;
    public override void SetActivePowerUp(bool isActive)
    {
        if (isActive)
        {
            Display.SetActive(false);
            GameManager.Instance.AddTimeRemaining(time);
        }
    }
}
