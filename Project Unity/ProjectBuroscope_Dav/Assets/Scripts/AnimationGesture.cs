using UnityEngine;
using System.Collections;

public class AnimationGesture : MonoBehaviour {

    private Animator anim;
    private NavMeshAgent parentNavA;

	void Awake ()
    {
        anim = GetComponent<Animator>();
        parentNavA = transform.parent.GetComponent<NavMeshAgent>();
    }
	
	void Update ()
    {
	    if (Mathf.Abs(parentNavA.velocity.x) > 0.1f || Mathf.Abs(parentNavA.velocity.z) > 0.1f)
            anim.SetBool("Move", true);
        else anim.SetBool("Move", false);
    }
}
