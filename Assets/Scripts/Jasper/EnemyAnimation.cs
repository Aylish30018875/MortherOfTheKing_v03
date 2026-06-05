using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    Animator myAnim;
    Transform parentPosition;
    Vector3 lastPosition;

    void Start()
    {
        myAnim = this.GetComponent<Animator>();
        parentPosition = transform.parent.transform;
        lastPosition = parentPosition.position;
    }

    void Update()
    {
        Vector3 delta = (parentPosition.position - lastPosition).normalized;
        myAnim.SetFloat("y", delta.x);
        myAnim.SetFloat("x", delta.y);
        lastPosition = parentPosition.position;
    }
}
