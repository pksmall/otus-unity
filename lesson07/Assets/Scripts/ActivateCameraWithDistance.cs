using Cinemachine;
using UnityEngine;

public sealed class ActivateCameraWithDistance : MonoBehaviour
{
    [SerializeField] private CinemachineImpulseSource _objectToCheck;
    [SerializeField] private CinemachineVirtualCameraBase _switchCameraTo;
    private CinemachineBrain _cinemachineBrain;
    private float _distanceToObject;

    private void Start()
    {
        _distanceToObject = _objectToCheck.m_ImpulseDefinition.m_DissipationDistance;
        _cinemachineBrain = Camera.main.GetComponent<CinemachineBrain>();
    }

    private void Update()
    {
        if ((transform.position - _objectToCheck.transform.position).sqrMagnitude < _distanceToObject * _distanceToObject)
        {
            SwitchCamera(_switchCameraTo);
        }
    }

    private void SwitchCamera(CinemachineVirtualCameraBase vcam)
    {
        if (!_cinemachineBrain.ActiveVirtualCamera.Name.Equals(vcam.Name))
        {
                --_cinemachineBrain.ActiveVirtualCamera.Priority; 
        }
    }
}

