using UnityEngine;
using UnityEngine.InputSystem;

public sealed class AppController : MonoBehaviour
{
    void Update()
    {
#if UNITY_EDITOR
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
            UnityEditor.EditorApplication.ExitPlaymode();
#endif
    }
}
