using Cinemachine;
using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;


public sealed class ObjectActivator : MonoBehaviour
{
    [SerializeField] private TagType _activatorTag;
    [SerializeField] private bool _deactivateOnExit;
    [SerializeField] private GameObject[] _objects;
    [SerializeField] private CinemachineTargetGroup _targetGroup;
    private CinemachineVirtualCamera _virtCam;
    private float time = 3;

    private IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(TagManager.GetTag(_activatorTag)))
        {
            for (var index = 0; index < _objects.Length; index++)
            {
                var obj = _objects[index];
                obj.SetActive(true);
            }
        }

            
        if (_targetGroup)
        {
            // group camera
            _virtCam = _objects[0].GetComponent<CinemachineVirtualCamera>();

            foreach (var targetGroupMTarget in _targetGroup.m_Targets)
            {
                if (targetGroupMTarget.target.name != "Character") {
                    _virtCam.Follow = targetGroupMTarget.target;
                    Debug.Log(targetGroupMTarget.target.name);
                    yield return new WaitForSeconds(time);
                }
            }
        }
            
        // _targetGroup.m_Targets.Add(target)
    }

    // async wait
    private async void WaitTimer(float time)
    {
        string str = string.Format("Waiting {0} second...", time);
        Debug.Log(str);
        await Task.Delay(TimeSpan.FromSeconds(time));
        Debug.Log("Done!");
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_deactivateOnExit && collision.CompareTag(TagManager.GetTag(_activatorTag)))
        {
            for (var index = 0; index < _objects.Length; index++)
            {
                var obj = _objects[index];
                obj.SetActive(false);
            }
        }
    }

    private void OnBecameInvisible()
    {
    }

    private void OnBecameVisible()
    {
    }
}
