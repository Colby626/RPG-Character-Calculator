using UnityEngine;

public class NewNodeButton : MonoBehaviour
{
    public GameObject node;
    public Canvas skilltreeCanvas;

    public void CreateNode()
    {
        GameObject instance = Instantiate(node, Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 0.0f)), Quaternion.identity, skilltreeCanvas.transform);
        instance.transform.position = new Vector3(instance.transform.position.x, instance.transform.position.y, 0.0f);
    }
}
