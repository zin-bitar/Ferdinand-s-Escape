using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5.0f;
    private CharacterController controller;
    private Animator animator;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        if (controller == null)
        {
            Debug.LogError("CharacterController not found on " + gameObject.name);
        }

        if (animator == null)
        {
            Debug.LogError("Animator not found on " + gameObject.name);
        }
    }

    void Update()
    {
        MovePlayer();
        UpdateAnimation();
    }

    void MovePlayer()
    {
        float moveZ = Input.GetAxis("Vertical");
        float moveX = Input.GetAxis("Horizontal");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        controller.Move(move * speed * Time.deltaTime);
    }

    void UpdateAnimation()
    {
        bool isMoving = Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0;
        animator.SetBool("Marche", isMoving);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Porte"))
        {
            OuverturePorte ouverturePorte = other.gameObject.GetComponent<OuverturePorte>();
            if (ouverturePorte != null)
            {
                ouverturePorte.OuvrirPorte();
            }
        } else if (other.gameObject.CompareTag("Finish"))
        {
            FindObjectOfType<UIController>().GameOver(true);
        } else if (other.gameObject.CompareTag("Robot"))
        {
            FindObjectOfType<UIController>().GameOver(false);
        }
    }
}
