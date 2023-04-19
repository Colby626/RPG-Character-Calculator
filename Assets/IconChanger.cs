using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Events;

public class IconEvent : UnityEvent<Sprite> { }

public class IconChanger : MonoBehaviour
{
    public Image icon;
    [HideInInspector]
    public IconEvent iconEvent = new IconEvent();

    private void Start()
    {
        iconEvent.AddListener(ChangeIcon);
    }

    public void ChangeIcon(Sprite sprite)
    {
        icon.sprite = sprite;
    }

    //public void AssignListeners()
    //{
    //    Debug.Log("Assign Called");

    //    Debug.Log(FindObjectsOfType<IconButton>().Length);

    //    foreach (var button in FindObjectsOfType<IconButton>())
    //    {
    //        Debug.Log(button.name);
    //        button.iconEvent.AddListener(ChangeIcon);
    //    }
    //    //StartCoroutine(Timer());
    //}

    //IEnumerator Timer()
    //{
    //    yield return new WaitForSeconds(3f);

    //}
}
