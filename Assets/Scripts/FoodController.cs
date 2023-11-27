using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FoodController : MonoBehaviour
{
    [SerializeField] FoodItem[] foodOptions;
    [SerializeField] PowerUp[] powerUpOptions;
    [SerializeField] Transform[] dropPoints;
    [SerializeField] float dropRate;
    [SerializeField] float powerUpDropRate;

    private float minDropRate = .1f;
    private float timeSinceLastDrop = 0;
    private float timeSinceLastPowerupDrop = 0;

    // Start is called before the first frame update
    // Update is called once per frame

    void Update()
    {
        if (GameManager.Instance.GetIsGameRunning())
        {
            timeSinceLastDrop += Time.deltaTime;
            timeSinceLastPowerupDrop += Time.deltaTime;

            if (timeSinceLastDrop > Mathf.Max(minDropRate, dropRate - (.1f * GameManager.Instance.GetCurrentLevel())))
            {
                DropRandomItem();
                timeSinceLastDrop = 0;
            }

            if (timeSinceLastPowerupDrop > powerUpDropRate)
            {
                DropRandomPowerup();
                timeSinceLastPowerupDrop = 0;
            }
        }
    }


    /*
    void DropRandomItem()
    {
        var foodItems = transform.GetComponentsInChildren<FoodItem>();

        var item = foodItems[Random.Range(0, foodItems.Count())];
        var rb = item.GetComponent<Rigidbody>();
        rb.AddForce(new Vector3(0, .1f, -1) * dropForce, ForceMode.Impulse);
    }*/

    void DropRandomItem()
    {
        var item = foodOptions[Random.Range(0, foodOptions.Count())];
        var dropPoint = dropPoints[Random.Range(0, dropPoints.Count())];
        var newItem = Instantiate(item, dropPoint);

        var randomNum = Random.Range(0, 1);

        if (GameManager.Instance.GetComboCount() >= 9 && randomNum == 0)
        {
            newItem.transform.localScale = newItem.transform.localScale * 3;
            newItem.SetWeight(newItem.GetWeight() * 4);
        }
        else if (GameManager.Instance.GetComboCount() >= 6 && randomNum == 0)
        {
            newItem.transform.localScale = newItem.transform.localScale * 2;
            newItem.SetWeight(newItem.GetWeight() * 3);
        }
        else if (GameManager.Instance.GetComboCount() >= 3 && randomNum == 0)
        {
            newItem.transform.localScale = newItem.transform.localScale * 2;
            newItem.SetWeight(newItem.GetWeight() * 2);
        }
    }

    void DropRandomPowerup()
    {
        var powerUpOptionsTemp = powerUpOptions.Where(p => p.DisplayText != GameManager.Instance.GetActivePowerupName()).ToArray();

        if (GameManager.Instance.GetCurrentLevel() < 3)
        {
            powerUpOptionsTemp = powerUpOptionsTemp.Where(p => p.DisplayText != "Extra Time!").ToArray();
        }

        if (GameManager.Instance.GetCurrentLevel() < 5)
        {
            powerUpOptionsTemp = powerUpOptionsTemp.Where(p => p.DisplayText != "Gravity Slowed!").ToArray();
        }

        if (powerUpOptionsTemp.Count() > 0)
        {

            var item = powerUpOptionsTemp[Random.Range(0, powerUpOptionsTemp.Count())];
            var dropPoint = dropPoints[Random.Range(0, dropPoints.Count())];
            var newItem = Instantiate(item, dropPoint);
        }
    }
}
