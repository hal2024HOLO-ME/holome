using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowBall : MonoBehaviour
{
    public GameObject Ball;
    public void OnClickBallButton()
    {
        Ball.SetActive(true);
    }
}
