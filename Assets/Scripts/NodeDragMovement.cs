using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using SuperPupSystems.Helper;

public class DescriptionTextEvent : UnityEvent<string> { }

public class NodeDragMovement : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject descriptionText;
    public float timeToDescription = 1.2f;
    private Image image;
    private GameObject optionsMenu = null;
    public static GameObject currentNode;
    [HideInInspector]
    public DescriptionTextEvent textEvent = new DescriptionTextEvent();
    [HideInInspector]
    public UnityEvent removeConnectionsEvent = new UnityEvent();

    public void Start()
    {
        image = GetComponent<Image>();
        textEvent.AddListener(OnDescriptionChange);
        GetComponent<Timer>().TimeOut.AddListener(ShowDescription);
        removeConnectionsEvent.AddListener(OnRemoveConnections);
    }

    private void OnRemoveConnections()
    {
        foreach (Connector connector in GetComponentsInChildren<Connector>())
        {
            connector.ClearLines();
        }

        Destroy(optionsMenu);
    }

    private void OnDescriptionChange(string _text)
    {
        descriptionText.GetComponentInChildren<TextMeshProUGUI>().text = _text;
        Destroy(optionsMenu);
    }

    private void ShowDescription()
    {
        if (descriptionText.GetComponentInChildren<TextMeshProUGUI>().text != "")
            descriptionText.SetActive(true);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (descriptionText.GetComponentInChildren<TextMeshProUGUI>().text != null)
        {
            GetComponent<Timer>().StartTimer(timeToDescription, false);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (descriptionText.GetComponentInChildren<TextMeshProUGUI>().text != null)
        {
            GetComponent<Timer>().StopTimer();
        }

        descriptionText.SetActive(false);
    }

    private void Update()
    {
        if(!EventSystem.current.IsPointerOverGameObject() && Input.GetMouseButton(0) && optionsMenu != null)
        {
            currentNode = null;
            Destroy(optionsMenu);
        }
    }

    public void OnBeginDrag(PointerEventData pointerEventData)
    {
        if (pointerEventData.button == PointerEventData.InputButton.Left)
        {
            image.raycastTarget = false;
        }
    }

    public void OnDrag(PointerEventData pointerEventData)
    {
        if (pointerEventData.button == PointerEventData.InputButton.Left)
        {
            transform.position = (Vector2)Camera.main.ScreenToWorldPoint(pointerEventData.position);
        }
    }

    public void OnEndDrag(PointerEventData pointerEventData)
    {
        if (pointerEventData.button == PointerEventData.InputButton.Left)
        {
            image.raycastTarget = true;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right && optionsMenu == null && eventData.pointerClick.CompareTag("Node"))
        {
            currentNode = gameObject;
            optionsMenu = Instantiate(Resources.Load("NodeOptionsMenu") as GameObject);

            optionsMenu.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform);
            optionsMenu.transform.localScale = Vector3.one;
            optionsMenu.GetComponent<RectTransform>().transform.position = Camera.main.ScreenToWorldPoint(eventData.position);
            optionsMenu.transform.position = new Vector3(optionsMenu.transform.position.x, optionsMenu.transform.position.y, 1f);

            FindObjectOfType<DeleteNode>().destroyEvent.AddListener(Delete);
        }
        else if (eventData.button == PointerEventData.InputButton.Right && optionsMenu == null && eventData.pointerClick.CompareTag("Line"))
        {
            Debug.Log("Test");
            optionsMenu = Instantiate(Resources.Load("ConnectorOptionsMenu") as GameObject);

            optionsMenu.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform);
            optionsMenu.transform.localScale = Vector3.one;
            optionsMenu.GetComponent<RectTransform>().transform.position = Camera.main.ScreenToWorldPoint(eventData.position);
            optionsMenu.transform.position = new Vector3(optionsMenu.transform.position.x, optionsMenu.transform.position.y, 1f);

            FindObjectOfType<DeleteConnector>().destroyConnectorEvent.AddListener(DeleteConnector);
        }
        else if (eventData.button == PointerEventData.InputButton.Right && optionsMenu != null)
        {
            optionsMenu.GetComponent<RectTransform>().transform.position = Camera.main.ScreenToWorldPoint(eventData.position);
            optionsMenu.transform.position = new Vector3(optionsMenu.transform.position.x, optionsMenu.transform.position.y, 1f);
        }
        else if (eventData.button == PointerEventData.InputButton.Left && optionsMenu != null)
        {
            currentNode = null;
            Destroy(optionsMenu);
        }
    }

    private void Delete()
    {
        Destroy(optionsMenu);
        Destroy(gameObject);
    }

    private void DeleteConnector()
    {
        Destroy(optionsMenu);
        Destroy(gameObject);
    }
}
