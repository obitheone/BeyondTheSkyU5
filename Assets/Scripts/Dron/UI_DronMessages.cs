using UnityEngine;
using System.Collections.Generic;

public class UI_DronMessages : MonoBehaviour {

    public static UI_DronMessages Instance;

    public Dictionary<int,string> messages = new Dictionary<int, string>();

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        messages.Add(0, "HOLA PEPE CABRON");
    }

    public string GetMessage(int ID)
    {
        return messages[ID];
    }
}
