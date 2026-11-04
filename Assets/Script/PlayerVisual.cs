using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    private Animator animator;
    private Rigidbody rb;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponentInParent<Rigidbody>();
        animator.SetFloat("MotionSpeed", 1f);
    }

    private void Update()
    {
        Vector3 flat = rb.linearVelocity;
        flat.y = 0f;
        bool grounded = Mathf.Abs(rb.linearVelocity.y) < 0.1f;

        animator.SetFloat("Speed", flat.magnitude);
        animator.SetBool("Grounded", grounded);
        animator.SetBool("FreeFall", !grounded);

        if (flat.sqrMagnitude > 0.01f)
            transform.rotation = Quaternion.LookRotation(flat);
    }

    public void OnFootstep(AnimationEvent e) { }
    public void OnLand(AnimationEvent e) { }
}