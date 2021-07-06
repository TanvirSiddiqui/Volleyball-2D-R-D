using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Vector2 playerPosition = new Vector3(-3.64619994f, -2.6552999f);
    public Vector2 playerSideBallPosition = new Vector2(-2.4f, 0.61f);
    public Vector2 opponentSideBallPosition = new Vector2(2.98f, 0.39f);

    public GameObject player;
    public GameObject Opponent;
    [Range(1.1f, 8.1f)]
    public float startingX = 5.0f;
    public GameObject ball;
    public GameObject ballClone;

    public int whichSide = 1;
    public int playerPoint = 0;
    public int opponentPoint = 0;


    [SerializeField] TextMeshProUGUI PlayerScoreText;
    [SerializeField] TextMeshProUGUI EnemyScoreText;
    // Start is called before the first frame update
    void Start()
    {
        StartNewRound(whichSide);
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
    }


    public void StartNewRound(int whichSide)
    {

        if (whichSide == 1)
        {
            ballClone = Instantiate(ball, playerSideBallPosition, Quaternion.identity);
        }
        else if (whichSide == 2)
        {
            ballClone = Instantiate(ball, opponentSideBallPosition, Quaternion.identity);
        }

        ResetPositions();
    }

    public void DestroyBall()
    {
        Destroy(ballClone);

    }

    public void Score(int whichSide)
    {
        if (whichSide == 1)
        {
            playerPoint++;
            PlayerScoreText.text = playerPoint.ToString();
            DestroyBall();
            StartNewRound(whichSide);
            whichSide = 0;
        }
        else if (whichSide == 2)
        {
            opponentPoint++;
            EnemyScoreText.text = opponentPoint.ToString();
            DestroyBall();
            StartNewRound(whichSide);
            whichSide = 0;
        }
    }

    void CheckTouchLimit()
    {
    }

    public void ResetPositions()
    {
        player.transform.position = new Vector3(-3.64619994f, -1.52f);
        Opponent.transform.position = new Vector3(startingX, -1.59f);
        Debug.Log("Resetting position");
    }
}
