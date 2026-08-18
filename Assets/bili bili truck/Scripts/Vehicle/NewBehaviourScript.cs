using UnityEngine;

public class GamepadTest : MonoBehaviour
{
    void Update()
    {
        Debug.Log("H: " + Input.GetAxis("Horizontal"));
        Debug.Log("V: " + Input.GetAxis("Vertical"));
    }
}