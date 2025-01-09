using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.VFX;

public sealed class Throttle : MonoBehaviour
{
    [SerializeField] InputAction _trigger = null;

    void OnEnable()
      => _trigger.Enable();

    void OnDisable()
      => _trigger.Disable();

    void Update()
      => GetComponent<VisualEffect>()
           .SetFloat("Throttle", _trigger.ReadValue<float>());
}
