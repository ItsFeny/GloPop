using UnityEngine;

public class PlayerSetup : MonoBehaviour
{
    public PlayerController controller;

    public void isLocalPlayer()
    {
        controller.enabled = true;
    }
}
