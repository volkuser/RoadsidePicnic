using UnityEngine;
public class UILockSystemController : MonoBehaviour
{
    [SerializeField] private GameObject thisObject;
    
    private Animator _animator;
    private static readonly int Use = Animator.StringToHash("Use");

    private void Awake() => _animator = GetComponent<Animator>();
    
    private void AfterAnimation()
    {
        thisObject.SetActive(false);
        _animator.ResetTrigger(Use);
    }
}
