using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    Animator myAnim;
    Transform parentPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myAnim = this.GetComponent<Animator>();
        parentPosition = transform.parent.transform;
    }

    // Update is called once per frame
    void Update()
    {
        myAnim.SetFloat("y", Mathf.Clamp(parentPosition.position.y/100,-1,1));
        myAnim.SetFloat("x", Mathf.Clamp(parentPosition.position.x/100,-1,1));
    }
}
