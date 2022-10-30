using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.VFX.Utility;

[AddComponentMenu("VFX/Property Binders/Transform History Binder")]
[VFXBinder("Transform/Transform History")]
sealed class TransformHistoryBinder : VFXBinderBase
{
    public string CurrentProperty
      { get => (string)_currentProperty;
        set => _currentProperty = value; }

    public string PreviousProperty
      { get => (string)_previousProperty;
        set => _previousProperty = value; }

    [VFXPropertyBinding("UnityEditor.VFX.Transform"), SerializeField]
    ExposedProperty _currentProperty = "CurrentTransform";

    [VFXPropertyBinding("UnityEditor.VFX.Transform"), SerializeField]
    ExposedProperty _previousProperty = "PreviousTransform";

    [SerializeField] Transform _target = null;

    (ExposedProperty p, ExposedProperty r, ExposedProperty s) _props1, _props2;
    (Vector3 p, Vector3 r, Vector3 s) _xform1, _xform2;

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

    void LateUpdate()
      => UpdateTransformCache();

    void UpdateSubProperties()
    {
        _props1 = (_currentProperty + "_position",
                   _currentProperty + "_angles",
                   _currentProperty + "_scale");
        _props2 = (_previousProperty + "_position",
                   _previousProperty + "_angles",
                   _previousProperty + "_scale");
    }

    void UpdateTransformCache()
    {
        if (_target == null) return;
        _xform2 = _xform1;
        _xform1 = (_target.position, _target.eulerAngles, _target.localScale);
    }

    public override bool IsValid(VisualEffect component)
      => _target != null &&
         component.HasVector3((int)_props1.p) &&
         component.HasVector3((int)_props1.r) &&
         component.HasVector3((int)_props1.s) &&
         component.HasVector3((int)_props2.p) &&
         component.HasVector3((int)_props2.r) &&
         component.HasVector3((int)_props2.s);

    public override void UpdateBinding(VisualEffect component)
    {
        component.SetVector3((int)_props1.p, _xform1.p);
        component.SetVector3((int)_props1.r, _xform1.r);
        component.SetVector3((int)_props1.s, _xform1.s);
        component.SetVector3((int)_props2.p, _xform2.p);
        component.SetVector3((int)_props2.r, _xform2.r);
        component.SetVector3((int)_props2.s, _xform2.s);
    }

    public override string ToString()
      => $"Transform : '{_currentProperty}', '{_previousProperty}' -> {TargetName}";
}
