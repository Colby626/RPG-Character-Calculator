using UnityEngine;
using TMPro;

public class SubmitDescriptionButton : MonoBehaviour
{
    public void SubmitText()
    {
        NodeDragMovement.currentNode.GetComponent<NodeDragMovement>().textEvent.Invoke(GetComponentInParent<TMP_InputField>().text);
    }
}
