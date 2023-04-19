using UnityEngine;
using UnityEngine.Events;

public class DeleteNode : MonoBehaviour
{
    public UnityEvent destroyEvent;

    public void InvokeDestroy()
    {
        destroyEvent.Invoke();
    }
}
