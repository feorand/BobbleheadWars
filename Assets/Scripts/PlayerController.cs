using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float moveSpeed = 50.0f;
    public Rigidbody head;
    public LayerMask layerMask;

    private CharacterController characterController;
    private Vector3 currentLookTarget = Vector3.zero;

	// Use this for initialization
	void Start () {
        characterController = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
        var moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        characterController.SimpleMove(moveDirection * moveSpeed);
	}

    private void FixedUpdate()
    {
        var moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        if (moveDirection == Vector3.zero) 
        {
            //TODO Standing still
        }
        else
        {
            head.AddForce(transform.right * 150, ForceMode.Acceleration);
        }

        RaycastHit hit;
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 1000, Color.green);

        if (Physics.Raycast(ray, out hit, 1000, layerMask, QueryTriggerInteraction.Ignore))
        {
            if (hit.point != currentLookTarget)
            {
                currentLookTarget = hit.point;
            }

            var targetPosition = new Vector3(hit.point.x, transform.position.y, hit.point.z);
            var rotation = Quaternion.LookRotation(targetPosition - transform.position);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * 10.0f);
        }
    }
}

