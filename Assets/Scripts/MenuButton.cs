using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    [SerializeField] Sprite DefaultBackground;
    [SerializeField] Sprite HoverBackground;

    Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }
    public void OnMouseOver()
    {
        image.sprite = HoverBackground;
    }

    public void OnMouseExit()
    {
        image.sprite = DefaultBackground;
    }
}
