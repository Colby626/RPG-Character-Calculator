using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ContentFiller : MonoBehaviour
{
    public List<Sprite> icons = new List<Sprite>();

    private void Start()
    {
        foreach (Sprite icon in icons)
        {
            GameObject go = Instantiate(Resources.Load("IconOption") as GameObject);
            go.transform.SetParent(transform);
            go.transform.localScale = Vector3.one;
            go.GetComponentInChildren<Image>().sprite = icon;
        }
    }
}
