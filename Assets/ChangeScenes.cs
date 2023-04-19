using UnityEngine;

public class ChangeScenes : MonoBehaviour
{
    public GameObject characterCards;
    public GameObject skillTree;

    public void Change()
    {
        if (characterCards.activeSelf)
        {
            characterCards.SetActive(false);
            skillTree.SetActive(true);
        }
        else
        {
            characterCards.SetActive(true);
            skillTree.SetActive(false);
        }
    }
}
