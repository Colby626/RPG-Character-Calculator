using UnityEngine;

public class CharacterStatsInput : MonoBehaviour
{
    public void RemoveVariable(GameObject caller)
    {
        RectTransform rect = caller.transform.parent.transform.parent.GetComponentInParent<RectTransform>();
        Debug.Log(rect.gameObject.name);
        rect.sizeDelta = new Vector2(rect.sizeDelta.x, rect.sizeDelta.y - 25);
        Destroy(caller);
    }
}
