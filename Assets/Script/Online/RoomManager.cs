using Photon.Pun;
using System.Collections;
using TMPro;
using UnityEngine;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public GameObject PlayerPrefab;
    public Transform SpawnPoint1;
    public Transform SpawnPoint2;
    public GameObject cuerdaObject;
    public TextMeshProUGUI countdownText;
    public Canvas infoCanvas; // Agrega referencia al Canvas que deseas activar al final del contador
    public Vector3 playerRotation = new Vector3(0f, 263.509f, 0f);

    private void Start()
    {
        Debug.Log("Conectando");
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        Debug.Log("Conectado Al Servidor");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        Debug.Log("En el lobby");
        PhotonNetwork.JoinOrCreateRoom("Test", null, null);
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log("En la sala!");

        Transform spawnPoint = PhotonNetwork.CurrentRoom.PlayerCount == 1 ? SpawnPoint1 : SpawnPoint2;
        GameObject player = PhotonNetwork.Instantiate(PlayerPrefab.name, spawnPoint.position, Quaternion.Euler(playerRotation));
        player.GetComponent<PlayerSetup>().isLocalPlayer();

        if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            StartCoroutine(CountdownCoroutine());
        }
    }

    private IEnumerator CountdownCoroutine()
    {
        int countdown = 3;
        while (countdown > 0)
        {
            countdownText.text = countdown.ToString();
            yield return new WaitForSeconds(1f);
            countdown--;
        }
        countdownText.text = "YA!!";
        yield return new WaitForSeconds(1f); // Espera un segundo después de mostrar "YA!!"
        countdownText.gameObject.SetActive(false); // Desactiva el texto
        cuerdaObject.SetActive(true); // Activa la cuerda

        // Activa el Canvas después de activar la cuerda
        photonView.RPC("ActivateInfoCanvas", RpcTarget.All);
    }

    [PunRPC]
    void ActivateInfoCanvas()
    {
        infoCanvas.gameObject.SetActive(true);
    }
}
