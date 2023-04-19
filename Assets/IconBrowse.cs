using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class IconBrowse : MonoBehaviour
{
    public GameObject menuParent;
    public static IconBrowse Instance;
    [HideInInspector]
    public bool isOpen = false;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        isOpen = true;
    }

    private void Start()
    {
        foreach(var button in FindObjectsOfType<IconButton>())
        {
            button.GetComponent<Button>().onClick.AddListener(Close);
        }
    }

    private void Close()
    {
        Destroy(FindObjectOfType<OpenSubMenu>().gameObject);
        Destroy(menuParent);
    }

    private void OnDestroy()
    {
        Instance = null;
        isOpen = false;
    }
}
