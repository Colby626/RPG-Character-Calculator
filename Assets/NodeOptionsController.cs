using UnityEngine;
using UnityEngine.EventSystems;

public class NodeOptionsController : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            GameObject instance = Instantiate(Resources.Load("OptionsMenu") as GameObject);

            //instance.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform);
            //instance.transform.localScale = Vector3.one;
            //instance.transform.localPosition = eventData.position;
        }
    }
}
