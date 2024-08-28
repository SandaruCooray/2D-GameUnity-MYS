using UnityEngine;

public class ChaseState : StateMachineBehaviour
{
    Transform target;
    public float speed = 3;
    Transform borderCheck;
    Zombie zombieScript;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            target = player.transform;
        }
        zombieScript = animator.GetComponent<Zombie>();
        borderCheck = animator.GetComponent<Zombie>().borderCheck;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!zombieScript.isStopped && target != null)
        {
            Vector2 newPos = new Vector2(target.position.x, animator.transform.position.y);
            animator.transform.position = Vector2.MoveTowards(animator.transform.position, newPos, speed * Time.deltaTime);

            if (!Physics2D.Raycast(borderCheck.position, Vector2.down, 2))
            {
                animator.SetBool("isChasing", false);
            }

            float distance = Vector2.Distance(target.position, animator.transform.position);
            if (distance < 1.5f)
            {
                animator.SetBool("isAttacking", true);
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

}
