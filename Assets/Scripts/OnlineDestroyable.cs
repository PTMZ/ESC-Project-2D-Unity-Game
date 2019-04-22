using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class OnlineDestroyable : MonoBehaviourPun, IPunObservable
{
    public GameObject FloatingTextPrefab;
    private int currentHealth = 100;
    // Start is called before the first frame update
    
    public void reduceHealthRPC(int damage)    
    {
        PhotonView photonView = PhotonView.Get(this);
        photonView.RPC("reduceHealth", RpcTarget.All, damage);
        Debug.Log("Reduce health Called");
        
    }

    [PunRPC]
    public void reduceHealth(int damage, PhotonMessageInfo info)
    {
        PhotonView photonView = PhotonView.Get(this);
        Debug.Log("RPC reduce health Called");
        currentHealth -= damage;
        Debug.Log("CurrentHealth: " + currentHealth);
        showFloatingTextHealth();
        //Debug.Log("My life is: " + currentHealth);
    }

    void showFloatingTextHealth()
    {
        //Debug.Log("Show health");
        var go = Instantiate(FloatingTextPrefab, transform.position, Quaternion.identity, transform);
        go.GetComponent<TMPro.TextMeshPro>().text = currentHealth.ToString();
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(currentHealth);
        }
        else
        {
            this.currentHealth = (int)stream.ReceiveNext();
            //UpdateAnimation();
        }

    }
    
}
