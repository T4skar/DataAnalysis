using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasScript : MonoBehaviour
{
    [SerializeField] Dropdown playersDropdown;
    List<string> playerIds = new();

    void Start()
    {
        // Igualar playerIds a todos los userIds o sessionId que haya (Seguramente haya q pasar ints a string)

        playersDropdown.AddOptions(playerIds);
    }

    void Update()
    {
        
    }

    public void DropDown_SetEvent(int eventId)
    {

    }

    public void DropDown_SetPlayer(int playerId)
    {

    }

    public void Button_GetData()
    {

    }

    public void Button_StartHeatmap()
    {

    }

    public void Button_Restart()
    {

    }

    public void SetOpacity(float opacity)
    {
        GetComponent<CanvasGroup>().alpha = opacity;
    }
}
