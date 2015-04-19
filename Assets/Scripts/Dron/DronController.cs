using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum DronStates {Follow = 1, Talking}

public class DronController : MonoBehaviour {

    public static DronController Instance;
    public Transform followPos;
    public Transform talkingPos;
    public Transform messagePos;
	public float rotDamping = 3.0f;
    public float movDamping = 2.0f;
    public GameObject message;
    public string[] messagesText;
    public GameObject currentMessageText;

    private NavMeshAgent _agent;
    private DronStates _state;
    private bool _showingMessage;
    private int _numMessagesToShow;
    private int[] _messagesIDs;
    private int _currentMessageID;


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

    public void MessagesToShow(int num, int[] messageIDs)
    {
        _numMessagesToShow = num;
        _messagesIDs = messageIDs;
        //currentMessageText.GetComponent<Text>().text = messagesText[_messagesIDs[_currentMessageID]];
        currentMessageText.GetComponent<Text>().text = UI_DronMessages.Instance.GetMessage(_messagesIDs[_currentMessageID]);
    }

    public void ActiveNextMessage()
    {
        --_numMessagesToShow;
        ++_currentMessageID;
        if (_numMessagesToShow == 0)
        {
            message.SetActive(false);
            _state = DronStates.Follow;
            TP_Camera.Instance.modoCamara = CameraTypes.Follow;
            TP_Status.Instance.SetControllable(true);
        }
        else
        {
            //Cambiar al siguiente mensaje
            currentMessageText.GetComponent<Text>().text = UI_DronMessages.Instance.GetMessage(_messagesIDs[_currentMessageID]);
        }
    }

	void Start () {
		//agent = GetComponent<NavMeshAgent>();
		//agent.SetDestination(target.position);
        _state = DronStates.Follow;
        _showingMessage = false;
        message.SetActive(false);
        _currentMessageID = 0;
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
        if (test.magnitude < 0.1f && _state == DronStates.Talking && message.activeSelf == false)
        {
            message.SetActive(true);
        }
	}
	
}