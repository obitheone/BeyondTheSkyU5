using UnityEngine;
using System.Collections.Generic;

public class UI_DronMessages : MonoBehaviour {

    public static UI_DronMessages Instance;

    public Dictionary<string,string> messages = new Dictionary<string, string>();

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        messages.Add("1", "HOLA PEPE CABRON");
    }

    public string GetMessage(int ID)
    {
        return messages[ID.ToString()];
    }
}
