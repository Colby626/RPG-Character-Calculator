using UnityEngine;

public class CharacterStatsInput : MonoBehaviour
{
    public string key;
    public void RemoveVariable(GameObject caller)
    {
        RectTransform rect = caller.transform.parent.transform.parent.GetComponentInParent<RectTransform>();
        RectTransform childRect = caller.transform.parent.transform.parent.GetChild(0).GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(rect.sizeDelta.x, rect.sizeDelta.y - 25);
        childRect.sizeDelta = new Vector2(childRect.sizeDelta.x, childRect.sizeDelta.y - 25);
        rect.GetComponent<CharacterCard>().characterStats.Remove(key);
        Destroy(caller);
    }
}
