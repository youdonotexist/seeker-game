using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using InControl;


public class PlayerVehicle : MonoBehaviour 
{
	public Collider _collider = null;
	
	//Player Control
	public float maxForwardSpeed 	= 150.0f;
	public float maxSideSpeed 		= 20.0f;
	public float minForwardSpeed 	= 20.0f;
	public float maxBackwardSpeed 	= 50.0f;
	
	public float flightAltitude = 40.0f;
	
	public float thrustForce = 0.001f;
	
	public GameObject trueCenter;
	
	//Droppod Stuff
	public GameObject[] droppodPrefabs;
	public string[] droppodNames;
	Vector3 droppodPlacement;
	Dictionary<string, Object> nameToPrefab;
	
	//UI
	//UIRightClickPopupMenu _droppodMenu;
	//HealthBar _playerHealthUI;
	Dictionary<string, Object> currentRightclickHash = null;
	GameObject clickedObject = null;
	
	bool autopilot = false;
	bool fireInput = false;
	
	Vector3 mousePos;

	void Start() {
		InputManager.Setup();
		
		// Add a custom device profile.
		InputManager.AttachDevice( new UnityInputDevice( new FPSProfile() ) );
	}
	
	protected void Update() {
		UpdateMouse();
		UpdateInput();
	}
	
	public Collider CollisionHull() {
		if (_collider == null) {
			_collider = GetComponentInChildren<Collider>();
		}
		
		return _collider;
	}
	
	void UpdateMouse() {
		InputManager.Update();
		var inputDevice = InputManager.ActiveDevice;
		//float mouseX = (float) Input.mousePosition.x;
    	//float mouseY = (float) Input.mousePosition.y;
		float mouseX = inputDevice.Direction.x;
		float mouseY = inputDevice.Direction.y;

		Debug.Log("Mouse: + (" + mouseX + ", " + mouseY + ")");
		float distance = Mathf.Abs(Camera.main.transform.position.z - transform.position.z);
				
		mousePos = Camera.main.ScreenToWorldPoint(new Vector3(mouseX, mouseY, distance));
	}

	void UpdateInput() {
		if (autopilot == false) {
			UpdateShip();
		}
	}
	
	//Ship Rotation
	void UpdateShip() {
		//Vector3 forward = transform.forward;
		Vector3 tru = trueCenter.transform.position; tru.z = 0.0f;
		Vector3 mouseForward = (mousePos - trueCenter.transform.position).normalized;
		mouseForward.z = 0.0f;

		var inputDevice = InputManager.ActiveDevice;
		transform.up = -new Vector3(inputDevice.Direction.x, inputDevice.Direction.y, 0.0f);
	}
	
	void passiveRadar() {
		//_lastRadar += Time.deltaTime;
	}

	void OnDragGizmos() {
		Debug.DrawLine(transform.position, transform.position + (transform.up * 100.0f));
	}
}

