using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodItem : MonoBehaviour
{
    [SerializeField] float weightOunces;


    public float GetWeight()
    { return weightOunces; }

   public void SetWeight(float weightOunces)
    {
        this.weightOunces = weightOunces;
    }
}
