using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Food"))
        {
            Destroy(other.gameObject);

            GameManager.Instance.ResetComboCount();
        }
        else if (other.CompareTag("PowerUp"))
        {
            if (!GameManager.Instance.IsPowerUpActive())
            {
                Destroy(other.gameObject);
            }
        }
    }
}
