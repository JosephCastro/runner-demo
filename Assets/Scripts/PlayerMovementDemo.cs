using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerMovementDemo : MonoBehaviour {

    private Rigidbody rigidBody;
    public float runForce;
    public float jumpForce =100f;
	public bool canJump = true;
    private int counter = 0;
    public Text scoreDisplayer;
    GameObject[] items;

    private void Restart()
    {
        counter = 0;
        //scoreDisplayer.text = "SCORE: " + counter;
        transform.position = new Vector3(-10, -2, 0);       
        foreach (GameObject item in items)
        {
            item.SetActive(true);
        }
    }

    private void Jump()
    {
        rigidBody.AddForce(Vector3.up * jumpForce);
		canJump = false;
    }

    private void GoingForward()
    {
		rigidBody.MovePosition(transform.position + transform.forward * Time.deltaTime * runForce);
    }

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        //scoreDisplayer.text = "SCORE : " + counter;
        items = GameObject.FindGameObjectsWithTag("Pick Up");
    }

    // to run Physics
    private void FixedUpdate()
    {
        GoingForward();
		if (Input.GetKeyDown("space") && canJump)
        {
            Jump();
        }

       if (transform.position.y <= -4)
        {
            Restart();
        }

    }

    void OnTriggerEnter(Collider other)
    {
		if (other.gameObject.CompareTag ("Pick Up")) {
			other.gameObject.SetActive (false);
			counter++;
			//scoreDisplayer.text = "SCORE: " + counter;
		} else if (other.gameObject.CompareTag ("Enemy")) {
			Restart ();
		}
    }
		
	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.CompareTag ("Ground")) {
			canJump = true;
		}
	}

}