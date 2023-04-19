using UnityEngine;

public class EditDescriptionButton : MonoBehaviour
{
    public GameObject nodeParent;
    public GameObject textField;

    public void AddTextField()
    {
        GameObject instance = Instantiate(textField);
        instance.transform.SetParent(nodeParent.transform);
        instance.transform.localScale = Vector3.one;
    }
}
