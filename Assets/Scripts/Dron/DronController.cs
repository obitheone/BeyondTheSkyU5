using UnityEngine;
using System.Collections;

public enum DronStates {Follow = 1, Talking}

public class DronController : MonoBehaviour {

    public static DronController Instance;
    public Transform followPos;
    public Transform talkingPos;
    public Transform messagePos;
	public float rotDamping = 3.0f;
    public float movDamping = 2.0f;
    public GameObject message;

    private NavMeshAgent _agent;
    private DronStates _state;
    private bool _showingMessage;
    private int _messageID;

    void Awake()
    {
        Instance = this;
    }

    public void ActivateState(DronStates newState)
    {
        _state = newState;
    }

    public bool IsTalking()
    {
        return _state == DronStates.Talking;
    }

    public DronStates GetDronState()
    {
        return _state;
    }

    public void ShowMessage(int id)
    {
        _messageID = id;
    }

	void Start () {
		//agent = GetComponent<NavMeshAgent>();
		//agent.SetDestination(target.position);
        _state = DronStates.Follow;
        _showingMessage = false;
        message.SetActive(false);
	}

	
	void LateUpdate () 
    {
        switch (_state)
        {
            case DronStates.Follow:
                if (_showingMessage) _showingMessage = false;
                transform.position = Vector3.Lerp(transform.position, followPos.position, movDamping * Time.deltaTime);
		        transform.rotation = Quaternion.Slerp (transform.rotation, followPos.transform.rotation, rotDamping * Time.deltaTime);
                break;
            case DronStates.Talking:
                transform.position = Vector3.Lerp(transform.position, talkingPos.position, movDamping * Time.deltaTime);
		        //transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation(TP_Controller.Instance.transform.position - transform.position), rotDamping * Time.deltaTime);
                transform.LookAt(Camera.main.transform);
                break;
        }
        

        /*if (agent.enabled) 
		{
				
			//agent.SetDestination(target.position);
			agent.SetDestination(new Vector3 (wantedX, wantedY, wantedZ));
				
		}*/
	}


	void Update ()
	{
		/*float distancia = Vector3.Distance (target.position, transform.position);
		Debug.Log (distancia);
		if (distancia > 10)
						agent.enabled = false;
		else
						
			if (distancia<2) agent.enabled = true;*/
        Vector3 test = transform.position - talkingPos.position;
        if (test.magnitude < 0.1f && _state == DronStates.Talking && !_showingMessage && _messageID > 0)
        {
            _showingMessage = true;
            //Instantiate(message, messagePos.position, Quaternion.identity);
            message.SetActive(true);
            _messageID = 0;
        }
        Debug.Log(test.magnitude);
	}
	
}