using UnityEngine;

public class StartMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject fullDarkening;
    
    private Animator _fullDarkeningAnimator;
    private static readonly int DarkenUp = Animator.StringToHash("DarkenUp");
    private void Awake() => _fullDarkeningAnimator = fullDarkening.GetComponent<Animator>();
    
    public void StartGame() => _fullDarkeningAnimator.SetTrigger(DarkenUp);

    public void Exit() => Application.Quit();
}
