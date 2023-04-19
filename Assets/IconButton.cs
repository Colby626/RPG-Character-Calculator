using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class IconButton : MonoBehaviour
{
    public void InvokeIconEvent()
    {
        NodeDragMovement.currentNode.GetComponent<IconChanger>().iconEvent.Invoke(GetComponent<Image>().sprite);
    }
}
