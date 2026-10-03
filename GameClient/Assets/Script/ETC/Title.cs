using UnityEngine;

public class Title : MonoBehaviour
{
    public void OnTouchLogin()
    {
       new OpenPopup<LoginPopup>("LoginPopup", Vector3.zero, transform);
    }


    public void OnTouchExit()
    {
        Application.Quit();
    }
}