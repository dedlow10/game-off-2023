using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityPowerUp : PowerUp
{
    public override void SetActivePowerUp(bool isActive)
    {
        if (isActive)
        {
            Display.SetActive(false);
            GameManager.Instance.SetPowerUpActive(this.DisplayText);
            Physics.gravity = new Vector3(0, -.3f, 0);
        }
        else
        {
            Display.SetActive(true);
            GameManager.Instance.SetPowerUpActive(null);
            Physics.gravity = new Vector3(0, -1f, 0);
        }
    }

    private void Update()
    {
        if (GameManager.Instance.IsPowerUpActive())
        {
            currentPowerUpLength += Time.deltaTime;

            if (currentPowerUpLength > PowerUpLength)
            {
                GameManager.Instance.SetPowerUpActive(null);
                Physics.gravity = new Vector3(0, -1f, 0);
                Destroy(gameObject);
            }
        }

    }
}
