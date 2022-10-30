using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.VFX.Utility;

[AddComponentMenu("VFX/Property Binders/Previous Transform Binder")]
[VFXBinder("Transform/Previous Transform")]
sealed class PreviousTransformBinder : VFXBinderBase
{
    public string Property
      { get => (string)_property; set => _property = value; }

    [VFXPropertyBinding("UnityEditor.VFX.Transform"), SerializeField]
    ExposedProperty _property = "OldTransform";

    [SerializeField] Transform _target = null;

    (ExposedProperty p, ExposedProperty r, ExposedProperty s) _props;
    (Vector3 p, Vector3 r, Vector3 s) _cache;

    string TargetName
      => _target == null ? "(null)" : _target.name;

    protected override void OnEnable()
    {
        base.OnEnable();
        UpdateSubProperties();
        UpdateTransformCache();
    }

    void OnValidate()
    {
        UpdateSubProperties();
        UpdateTransformCache();
    }

    void UpdateSubProperties()
      => _props = (_property + "_position",
                   _property + "_angles",
                   _property + "_scale");

    void UpdateTransformCache()
      => _cache = _target != null ?
           (_target.position, _target.eulerAngles, _target.localScale) :
           (Vector3.zero, Vector3.zero, Vector3.zero);

    public override bool IsValid(VisualEffect component)
      => _target != null &&
         component.HasVector3((int)_props.p) &&
         component.HasVector3((int)_props.r) &&
         component.HasVector3((int)_props.s);

    public override void UpdateBinding(VisualEffect component)
    {
        component.SetVector3((int)_props.p, _cache.p);
        component.SetVector3((int)_props.r, _cache.r);
        component.SetVector3((int)_props.s, _cache.s);
        UpdateTransformCache();
    }

    public override string ToString()
      => $"Transform : '{_property}' -> {TargetName}";
}
