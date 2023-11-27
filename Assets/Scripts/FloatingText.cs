using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    [SerializeField] float destroyTime = 4f;

    private void Start()
    {
        Destroy(gameObject, destroyTime);
    }
}
