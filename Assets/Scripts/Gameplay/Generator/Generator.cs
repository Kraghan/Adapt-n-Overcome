using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    [SerializeField]
    LevelStats m_levelParams;
    [SerializeField]
    PlayerStats m_playerParams;

    [SerializeField]
    Player m_player;

	// Use this for initialization
	void Awake ()
    {
        CalculateStyleOfPlay();
	}

    public void FixedUpdate()
    {
        if(Input.GetKey("h"))
        {
            CalculateStyleOfPlay();
        }
    }

    private void CalculateStyleOfPlay()
    {
        CalculateBombermanStyle();
        CalculateCommanderStyle();
        CalculateGunslingerStyle();
        CalculatePacifistStyle();
        CalculateSupermanStyle();
    }

    private void CalculateCommanderStyle()
    {

    }

    private void CalculateBombermanStyle()
    {

    }

    private void CalculateSupermanStyle()
    {

    }

    private void CalculateGunslingerStyle()
    {

    }

    private void CalculatePacifistStyle()
    {
        print(m_playerParams.GetAverageTimeBetweenBullets());
    }
}
