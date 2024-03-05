using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;

public class NetworkingManager : MonoBehaviourPunCallbacks
{
    public TMP_InputField InputcodigoCrear;
    public TMP_InputField InputcodigoEntrar;

    void Start()
    {
        // Conectar autom�ticamente al iniciar
        PhotonNetwork.ConnectUsingSettings();
    }

    public void CrearPartida()
    {
        string codigo = InputcodigoCrear.text;
        
        // Asegurar que el c�digo tiene m�ximo 10 caracteres
        if (codigo.Length <= 10)
        {
            // Crear una nueva sala con el c�digo especificado
            PhotonNetwork.CreateRoom(codigo);
        }
        else
        {
            Debug.LogWarning("El c�digo para crear la partida debe tener m�ximo 10 caracteres.");
        }
    }

    public void UnirseAPartida()
    {
        string codigo = InputcodigoEntrar.text;
        
        // Asegurar que el c�digo tiene m�ximo 10 caracteres
        if (codigo.Length <= 10)
        {
            // Unirse a una sala existente con el c�digo especificado
            PhotonNetwork.JoinRoom(codigo);
        }
        else
        {
            Debug.LogWarning("El c�digo para unirse a la partida debe tener m�ximo 10 caracteres.");
        }
    }

    // M�todos de callbacks de Photon
    public override void OnConnectedToMaster()
    {
        Debug.Log("Conectado al servidor de Photon");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.LogWarningFormat("Desconectado del servidor de Photon por la raz�n: {0}", cause);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.LogWarningFormat("Fall� al unirse a la sala: {0}", message);
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("Sala creada exitosamente");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Unido a la sala exitosamente");

        // Verificar si la sala est� llena
        if (PhotonNetwork.CurrentRoom.PlayerCount > 2)
        {
            Debug.LogWarning("La sala est� llena, no se pueden unir m�s jugadores.");
            PhotonNetwork.LeaveRoom();
        }
        else if (PhotonNetwork.CountOfPlayersInRooms >= 20)
        {
            Debug.LogWarning("Ya hay m�s de 20 jugadores simult�neamente. Por favor, espera a que una sala est� libre.");
            InputcodigoEntrar.interactable = false;
            InputcodigoEntrar.placeholder.GetComponent<TextMeshProUGUI>().text = "Tienes que esperar a que una sala est� libre";
        }
        else
        {
            // Habilitar el input para unirse a la partida
            InputcodigoEntrar.interactable = true;
            InputcodigoEntrar.placeholder.GetComponent<TextMeshProUGUI>().text = "C�digo para unirse a la partida";
        }
    }
}
