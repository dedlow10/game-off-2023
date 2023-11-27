using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartBasket : MonoBehaviour
{
    [SerializeField] GameObject CatchEffect;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Food"))
        {
            var food = other.GetComponent<FoodItem>();
            GameManager.Instance.IncrementComboCount();
            GameManager.Instance.AddToPlayerScore(food.GetWeight());

            var newParticle = Instantiate(CatchEffect, other.bounds.center, CatchEffect.transform.rotation);
            newParticle.GetComponent<ParticleSystem>().Play();
            if (food.GetWeight() < 30)
            {
                AudioManager.instance.PlaySFX("CatchLight", transform.position, .3f);
            }
            else if (food.GetWeight() < 50)
            {
                AudioManager.instance.PlaySFX("CatchMedium", transform.position, .3f);
            }
            else
            {
                AudioManager.instance.PlaySFX("CatchHeavy", transform.position, .3f);
            }


            if (GameManager.Instance.GetComboCount() == 3)
            {
                GameManager.Instance.ShowFloatingText(gameObject, "Nice!");
            }
            else if (GameManager.Instance.GetComboCount() == 6)
            {
                GameManager.Instance.ShowFloatingText(gameObject, "Wow!");
            }
            else if  (GameManager.Instance.GetComboCount() == 9)
            {
                GameManager.Instance.ShowFloatingText(gameObject, "Amazing!");
                AudioManager.instance.PlaySFX("Combo", transform.position, .3f);
            }
            else
            {
                GameManager.Instance.ShowFloatingText(gameObject, food.GetWeight().ToString());
            }

            StartCoroutine(DestroyAfter(2, newParticle));
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("PowerUp"))
        {
            AudioManager.instance.PlaySFX("PowerUp", transform.position, .3f);
            var powerUp = other.GetComponent<PowerUp>();
            powerUp.SetActivePowerUp(true);
            GameManager.Instance.ShowFloatingText(gameObject, powerUp.DisplayText);
        }
    } 

    private IEnumerator DestroyAfter(float seconds, GameObject go)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(go);
    }
}
