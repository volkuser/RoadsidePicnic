using UnityEngine;

public class EndScript : MonoBehaviour
{
    [SerializeField] private GameObject fullDarkening;
    private Animator _fullDarkeningAnimator;
    
    private static readonly int DarkenUp = Animator.StringToHash("DarkenUp");
    private void Awake() => _fullDarkeningAnimator = fullDarkening.GetComponent<Animator>();
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N)) return;
        _fullDarkeningAnimator.SetTrigger(DarkenUp);
    }
}
