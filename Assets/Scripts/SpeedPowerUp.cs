using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPowerUp : PowerUp
{
    [SerializeField] float speedBoost;
    public override void SetActivePowerUp(bool isActive)
    {
        if (isActive)
        {
            Display.SetActive(false);
            GameManager.Instance.SetPowerUpActive(this.DisplayText);
            GameManager.Instance.SetCartSpeedBoost(speedBoost);
        }
        else
        {
            Display.SetActive(true);
            GameManager.Instance.SetPowerUpActive(null);
            GameManager.Instance.SetCartSpeedBoost(1f);
        }
    }

    private void Update()
    {
        if (GameManager.Instance.IsPowerUpActive())
        {
            currentPowerUpLength += Time.deltaTime;

            if(currentPowerUpLength > PowerUpLength)
            {
                GameManager.Instance.SetCartSpeedBoost(1f);
                GameManager.Instance.SetPowerUpActive(null);
                Destroy(gameObject);
            }
        }

    }
}
