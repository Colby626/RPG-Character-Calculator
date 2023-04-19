using UnityEngine;

public class OpenSubMenu : MonoBehaviour
{
    public RectTransform optionsMenu;
    public GameObject subMenu;

    public void CreateSubMenu()
    {
        GameObject instance = Instantiate(subMenu);
        instance.transform.SetParent(transform);
        instance.transform.localPosition = new Vector3(optionsMenu.transform.position.x + optionsMenu.sizeDelta.x, optionsMenu.transform.position.y, 0.0f);
        instance.transform.localScale = Vector3.one;
    }
}
