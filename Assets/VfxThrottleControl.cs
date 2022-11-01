using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.VFX;

sealed class VfxThrottleControl : MonoBehaviour
{
    [SerializeField] InputAction _action = null;
    [SerializeField] string _propertyName = "Throttle";

    VisualEffect Vfx => GetComponent<VisualEffect>();

    float InputValue => _action?.ReadValue<float>() ?? 0;

    void OnEnable()
      => _action.Enable();

    void OnDisable()
      => _action.Disable();

    void Update()
      => Vfx.SetFloat(_propertyName, InputValue);
}
