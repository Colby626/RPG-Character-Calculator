using UnityEngine;

public class LineLock : MonoBehaviour
{
    public struct LineStatus
    {
        public bool isLocked;
        public RectTransform lockedRect;
    }

    [HideInInspector]
    public LineStatus lineStatus;
}
