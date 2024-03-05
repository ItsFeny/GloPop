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
        // Conectar automáticamente al iniciar
        PhotonNetwork.ConnectUsingSettings();
    }

    public void CrearPartida()
    {
        string codigo = InputcodigoCrear.text;
        
        // Asegurar que el código tiene máximo 10 caracteres
        if (codigo.Length <= 10)
        {
            // Crear una nueva sala con el código especificado
            PhotonNetwork.CreateRoom(codigo);
        }
        else
        {
            Debug.LogWarning("El código para crear la partida debe tener máximo 10 caracteres.");
        }
    }

    public void UnirseAPartida()
    {
        string codigo = InputcodigoEntrar.text;
        
        // Asegurar que el código tiene máximo 10 caracteres
        if (codigo.Length <= 10)
        {
            // Unirse a una sala existente con el código especificado
            PhotonNetwork.JoinRoom(codigo);
        }
        else
        {
            Debug.LogWarning("El código para unirse a la partida debe tener máximo 10 caracteres.");
        }
    }

    // Métodos de callbacks de Photon
    public override void OnConnectedToMaster()
    {
        Debug.Log("Conectado al servidor de Photon");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.LogWarningFormat("Desconectado del servidor de Photon por la razón: {0}", cause);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.LogWarningFormat("Falló al unirse a la sala: {0}", message);
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("Sala creada exitosamente");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Unido a la sala exitosamente");

        // Verificar si la sala está llena
        if (PhotonNetwork.CurrentRoom.PlayerCount > 2)
        {
            Debug.LogWarning("La sala está llena, no se pueden unir más jugadores.");
            PhotonNetwork.LeaveRoom();
        }
        else if (PhotonNetwork.CountOfPlayersInRooms >= 20)
        {
            Debug.LogWarning("Ya hay más de 20 jugadores simultáneamente. Por favor, espera a que una sala esté libre.");
            InputcodigoEntrar.interactable = false;
            InputcodigoEntrar.placeholder.GetComponent<TextMeshProUGUI>().text = "Tienes que esperar a que una sala esté libre";
        }
        else
        {
            // Habilitar el input para unirse a la partida
            InputcodigoEntrar.interactable = true;
            InputcodigoEntrar.placeholder.GetComponent<TextMeshProUGUI>().text = "Código para unirse a la partida";
        }
    }
}
