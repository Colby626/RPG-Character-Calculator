using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class Connector : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject connector;
    public Image highlight;
    public GameObject nodeParent;
    public float connectorWidth = .122f;
    public Material lineMaterial;
    public Gradient lineGradient;

    private LineRenderer line;
    private Connector otherConnector;
    private List<Connector> myConnectors;
    private RectTransform otherConnectorRect;
    private List<LineRenderer> lines = new List<LineRenderer>();
    private GameObject hoveredObj = null;

    private void Start()
    {
        myConnectors = new List<Connector>(nodeParent.GetComponentsInChildren<Connector>());
    }

    public void ClearLines()
    {
        foreach(var line in lines)
        {
            Destroy(line.gameObject);
        }

        lines.Clear();
    }

    public void OnBeginDrag(PointerEventData pointerEventData)
    {
        GameObject newConnector = Instantiate(connector);
        newConnector.transform.parent = transform.parent;

        line = newConnector.GetComponent<LineRenderer>();

        lines.Add(line);
    }

    public void OnDrag(PointerEventData pointerEventData)
    {
        if (myConnectors.Contains(pointerEventData.pointerDrag.GetComponentInChildren<Connector>()))
        {
            myConnectors.ForEach(connector => connector.highlight.enabled = false);
        }

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        line.SetPosition(0, GetComponent<RectTransform>().TransformPoint(GetComponent<RectTransform>().anchoredPosition));
        line.SetPosition(0, new Vector3(line.GetPosition(0).x, line.GetPosition(0).y, 0.0f));
        line.SetPosition(1, new Vector3(mousePos.x, mousePos.y, 0.0f));
    }

    public void OnEndDrag(PointerEventData pointerEventData)
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            foreach (GameObject obj in pointerEventData.hovered)
            {
                if (obj.CompareTag("Connector"))
                {
                    hoveredObj = obj;
                }
            }

            if (hoveredObj != null)
            {
                //find the other connector on the thing we hover
                otherConnector = hoveredObj.GetComponentInChildren<Connector>();

                //snap to central position of the other connector
                otherConnectorRect = otherConnector.GetComponent<RectTransform>();
                line.SetPosition(1, otherConnectorRect.TransformPoint(new Vector3(otherConnectorRect.anchoredPosition.x, otherConnectorRect.anchoredPosition.y, 0.0f)));
                
                //reset z again since above line doesn't for some reason
                line.SetPosition(1, new Vector3(line.GetPosition(1).x, line.GetPosition(1).y, 0.0f));

                //connection locked. Points will now be constantly updated to the position
                line.GetComponent<LineLock>().lineStatus.isLocked = true;
                line.GetComponent<LineLock>().lineStatus.lockedRect = otherConnectorRect;
            }
            else
            {
                //kill the line when it doesn't connect
                lines.Remove(line);
                Destroy(line.gameObject);
            }
        }
        else
        {
            //kill the line when it doesn't connect
            lines.Remove(line);
            Destroy(line.gameObject);
        }
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        highlight.enabled = true;
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        highlight.enabled = false;
    }

    private void Update()
    {
        //continually sets the positions the ones that were connected
        foreach (LineRenderer line in lines)
        {
            if (line == null)
            {
                continue;
            }

            if (line.GetComponent<LineLock>().lineStatus.isLocked && otherConnectorRect != null)
            {
                line.SetPosition(1, line.GetComponent<LineLock>().lineStatus.lockedRect.TransformPoint(new Vector3(otherConnectorRect.anchoredPosition.x, otherConnectorRect.anchoredPosition.y, 0.0f)));
                line.SetPosition(1, new Vector3(line.GetPosition(1).x, line.GetPosition(1).y, 0.0f));

                line.SetPosition(0, GetComponent<RectTransform>().TransformPoint(GetComponent<RectTransform>().anchoredPosition));
                line.SetPosition(0, new Vector3(line.GetPosition(0).x, line.GetPosition(0).y, 0.0f));
            }
            else if (line.GetComponent<LineLock>().lineStatus.isLocked && otherConnectorRect == null)
            {
                Destroy(line.gameObject);
            }
            else if (line.GetComponent<LineLock>() == null)
            {
                Destroy(line);
            }
        }

        lines.RemoveAll(line => line == null);
    }

    private void OnDestroy()
    {
        foreach (LineRenderer line in lines)
        {
            Destroy(line.gameObject);
        }

        lines.Clear();
    }
}
