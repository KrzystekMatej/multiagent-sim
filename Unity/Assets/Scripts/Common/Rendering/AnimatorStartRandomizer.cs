using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimatorStartRandomizer : MonoBehaviour
{
    private void Start()
    {
        Animator animator = GetComponent<Animator>();
        animator.Play(animator.GetCurrentAnimatorStateInfo(0).fullPathHash, 0, Random.value);
    }
}
