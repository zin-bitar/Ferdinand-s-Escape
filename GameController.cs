using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class GameController : MonoBehaviour
{
    private TimeController timeController;
    public GameObject[] movingRobots;
    public GameObject[] stationaryRobots;
    private List<Vector3[]> patrolPoints;
    public GameObject player;
    public GameObject door;
    private CharacterController controller;
    private bool playerSpotted;

    void Start()
    {
        patrolPoints = new List<Vector3[]>
        {
            new Vector3[] { new Vector3(-18, 0, -60), new Vector3(-13, 0, -65), new Vector3(-23, 0, -55) },
            new Vector3[] { new Vector3(0, 0, -60), new Vector3(5, 0, -65), new Vector3(-5, 0, -55) },
            new Vector3[] { new Vector3(12, 0, -60), new Vector3(7, 0, -65), new Vector3(17, 0, -55) },
            new Vector3[] { new Vector3(26, 0, -60), new Vector3(21, 0, -65), new Vector3(31, 0, -55) }
        };

        controller = player.GetComponent<CharacterController>();
        timeController = FindObjectOfType<TimeController>();
    }

    void Update()
    {
        UIController mode = FindObjectOfType<UIController>();
        if (timeController.seconds > 0)
        {
            playerSpotted = false;
            mode.mode.text = "Furtif";

            foreach (GameObject robot in movingRobots)
            {
                NavMeshAgent agent = robot.GetComponent<NavMeshAgent>();
                if (agent != null)
                {
                    if (Utilitaires.ObjetVisible(robot, player, 80.0f, 25.0f))
                    {
                        playerSpotted = true;
                        mode.mode.text = "Detecte";
                        break;
                    }
                }
            }

            foreach (GameObject robot in stationaryRobots)
            {
                NavMeshAgent agent = robot.GetComponent<NavMeshAgent>();
                if (agent != null)
                {
                    if (Utilitaires.ObjetVisible(robot, player, 80.0f, 25.0f))
                    {
                        playerSpotted = true;
                        mode.mode.text = "Detecte";
                        break;
                    }
                }
            }

            if (playerSpotted)
            {
                foreach (GameObject robot in movingRobots)
                {
                    NavMeshAgent agent = robot.GetComponent<NavMeshAgent>();
                    if (agent != null)
                    {
                        MoveTowardsPlayer(robot, agent, player.transform.position);
                    }
                }

                foreach (GameObject robot in stationaryRobots)
                {
                    NavMeshAgent agent = robot.GetComponent<NavMeshAgent>();
                    if (agent != null)
                    {
                        MoveTowardsPlayer(robot, agent, player.transform.position);
                    }
                }
            }
            else
            {
                foreach (GameObject robot in movingRobots)
                {
                    NavMeshAgent agent = robot.GetComponent<NavMeshAgent>();
                    if (agent != null)
                    {
                        Patrol(robot, agent);
                    }
                }

                foreach (GameObject robot in stationaryRobots)
                {
                    NavMeshAgent agent = robot.GetComponent<NavMeshAgent>();
                    if (agent != null)
                    {
                        StopMovement(robot, agent);
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.Y))
            {
                controller.enabled = false;
                player.transform.position = door.transform.position;
                controller.enabled = true;
            }
            if (Input.GetKeyDown(KeyCode.T))
            {
                timeController.seconds = 3;
            }
        }
        else
        {
            FindObjectOfType<UIController>().GameOver(false);
        }
    }

    void MoveTowardsPlayer(GameObject robot, NavMeshAgent agent, Vector3 targetPosition)
    {
        if (agent.isActiveAndEnabled && agent.isOnNavMesh)
        {
            robot.GetComponent<Animator>().SetBool("Marche", true);
            agent.SetDestination(targetPosition);
        }
    }

    void StopMovement(GameObject robot, NavMeshAgent agent)
    {
        if (agent.isActiveAndEnabled && agent.isOnNavMesh)
        {
            robot.GetComponent<Animator>().SetBool("Marche", false);
            agent.ResetPath();
        }
    }

    void Patrol(GameObject robot, NavMeshAgent agent)
    {
        if (agent.isActiveAndEnabled && agent.isOnNavMesh)
        {
            robot.GetComponent<Animator>().SetBool("Marche", true);
            int robotIndex = System.Array.IndexOf(movingRobots, robot);
            if (robotIndex >= 0 && robotIndex < patrolPoints.Count)
            {
                Vector3[] robotPatrolPoints = patrolPoints[robotIndex];
                if (robotPatrolPoints.Length > 0)
                {
                    if (!agent.hasPath || agent.remainingDistance < 0.5f)
                    {
                        Vector3 patrolPoint = robotPatrolPoints[Random.Range(0, robotPatrolPoints.Length)];
                        agent.SetDestination(patrolPoint);
                    }
                }
            }
        }
    }
}
