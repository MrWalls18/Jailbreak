using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolBehavior : StateMachineBehaviour
{
    private List<GameObject> patrolPoints = new List<GameObject>();
    private Transform currentPos, nextPos, lastPos;
    private int randomPointIndex;
    private bool isMoving;
    private NavMeshAgent enemyAgent;


    [SerializeField] private float maxDistance, speedMultiplier;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       patrolPoints.AddRange( GameObject.FindGameObjectsWithTag("PatrolPoint") );
        isMoving = false;

        enemyAgent = animator.GetComponent<NavMeshAgent>();

        
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(!isMoving)
        {
            GetNextPosition();  
        }
        
        

        MoveToPosition(animator);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    //Finds the next valid position based on the requirements
    //Will continue to run until valid position is found
    private void GetNextPosition()
    {
        randomPointIndex = Random.Range(0,patrolPoints.Count);
        nextPos = patrolPoints[randomPointIndex].transform;

        if (nextPos == currentPos ||
                nextPos == lastPos ||
                    Vector3.Distance(this.enemyAgent.transform.position, patrolPoints[randomPointIndex].transform.position) > maxDistance)
        {
            GetNextPosition();
        }
        else
        {
            lastPos = currentPos;
            currentPos = nextPos;
        }
    }


    ///////
    //Sets isMoving to true and waits until
    //enemy is close to patrolPoint to snap enemy to point
    ///////
    private void MoveToPosition(Animator anim)
    {
        if (!isMoving)
        {
            enemyAgent.destination = currentPos.position;
            
            isMoving = true;
        }

        ///////
        // 08/12/2021
        // Final distance is  about 0.25f
        // Temporary fix to leave < distance at 0.3f
        ///////
        if (Vector3.Distance(this.enemyAgent.transform.position, enemyAgent.destination) < 0.3f)
        {
            anim.SetBool("isPatrolling", false);
        }
    }



    #region Other State Methods
    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}

    #endregion Other State Methods

}
