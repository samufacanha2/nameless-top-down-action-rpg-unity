using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    Animator animator;

    public float hp = 2;

    public float HP
    {
        set
        {
            hp = value;

            if (hp <= 0)
            {
                animator.SetTrigger("defeated");

            }
        }
        get
        {
            return hp;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Hit()
    {
        print("hit");
        HP = HP - 1;

        animator.SetTrigger("hit");
    }


    public void F()
    {
        Destroy(gameObject);

    }


}
