using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour

{
    // Start is called before the first frame update
    public Animator anim;

    public Vector2 moveDirection;
    Vector2 previousPosition;

    void Start()
    {
        previousPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection = ((Vector2)transform.position - previousPosition).normalized;

        previousPosition = transform.position;

        anim.SetFloat("Horizontal", moveDirection.x);
        anim.SetFloat("Vertical", moveDirection.y);

    }
}
