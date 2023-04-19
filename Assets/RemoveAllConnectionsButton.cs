using UnityEngine;

public class RemoveAllConnectionsButton : MonoBehaviour
{
    public void RemoveConnections()
    {
        NodeDragMovement.currentNode.GetComponent<NodeDragMovement>().removeConnectionsEvent.Invoke();
    }
}
