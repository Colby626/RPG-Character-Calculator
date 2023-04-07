using UnityEngine;
using Photon.Pun;

public class SpawnCards : MonoBehaviour
{
    public GameObject card;
    public Canvas canvas;
    [HideInInspector]
    public Vector2 randomPos;
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    private void Start()
    {
        randomPos = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));

        PhotonNetwork.Instantiate(card.name, Vector2.zero, Quaternion.identity);
    }
}
