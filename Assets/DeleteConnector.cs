using UnityEngine;
using UnityEngine.Events;

public class DeleteConnector : MonoBehaviour
{
    public UnityEvent destroyConnectorEvent = new UnityEvent();

    public void DestroyConnector()
    {
        destroyConnectorEvent.Invoke();
    }
}
