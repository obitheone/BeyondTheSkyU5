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
        // D1-1 //
        messages.Add(0, "We’ve finally arrived to EZ 09, our assigned destination.");
        messages.Add(1, "Our studies indicate that this planet could be habitable. We’ve been sent here to confirm it.");
        messages.Add(2, "To do so we must recollect some samples while exploring the planet.");

        // D1-2 //
        messages.Add(3, "First of all let’s start doing a reconnaissance of the area.");
        messages.Add(4, "\"Move the right joystick to control the camera\"");

        // D1-3 //
        messages.Add(5, "My sensors are detecting a sample nearby...");
        messages.Add(6, "\"Try to find it by moving around with left stick\"");

        // D2-1 //
        messages.Add(7, "Brilliant! Let me scan it...");

        // D2-2 //
        messages.Add(8, "Woaaah! What was that? Follow it! Check it’s position in the map.");
        messages.Add(9, "\"Press \"select\" to open the map.\"");

        // D3-1 //
        messages.Add(10, "\"Press [JUMP_BUTTON] to jump.\"");

        // D4-1 //
        messages.Add(11, "I don’t think we can pull it up unless you use your new gravity gadget.");
        messages.Add(12, "\"To use it point a object i parlem-ho tots.\"");

        // DM1-1
        messages.Add(13, "I guess I could translate it.");
        messages.Add(14, "Could you tell us more about this planet? Is there more wildlife like you?");
    }

    public string GetMessage(int ID)
    {
        return messages[ID];
    }
}
