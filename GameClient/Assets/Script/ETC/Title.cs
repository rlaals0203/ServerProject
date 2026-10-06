using UnityEngine;

public class Title : MonoBehaviour
{
    public void OnTouchLogin()
    {
       var popup = new OpenPopup<LoginPopup>("LoginPopup", Vector3.zero, transform).spawnedPopup;
       popup.Rect.anchoredPosition = Vector3.zero;
    }


    public void OnTouchExit()
    {
        Application.Quit();
    }
}