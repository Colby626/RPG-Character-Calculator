using UnityEngine;
using Photon.Pun;

public class RectTransformView : MonoBehaviour, IPunObservable
{
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(GetComponent<RectTransform>().position);
        }
        else
        {
            GetComponent<RectTransform>().position = (Vector3)stream.ReceiveNext();
        }
    }
}
