using UnityEngine;

public class Title : MonoBehaviour
{
    public void OnTouchLogin()
    {
       new OpenPopup<LoginPopup>("LoginPopup", Vector3.zero);
    }


    public void OnTouchExit()
    {
        Application.Quit();
    }
}