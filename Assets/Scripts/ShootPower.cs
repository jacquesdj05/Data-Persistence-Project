using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShootPower : MonoBehaviour
{
    // TO DO:
    // add a timer
    // particle effects

    // When the player presses the mouse button down, calculate the difference between
    // the Vector3 of the ball and mouse so that, when the mouse button is released, a proportionate
    // force is applied to the ball - like a sling shot.

    [SerializeField] Rigidbody ballRb;

    [SerializeField] GameObject box;

    [SerializeField] float shotStrength = 10f;
    [SerializeField] float timerTime = 15f;
    [SerializeField] bool hasShot;

    Vector3 shotDirection;
    Vector3 worldPointDown;
    Vector3 worldPointUp;

    TrajectoryLine trajectoryLine;

    Counter counter;

    public bool isGameActive;

    public Text gameOverText;
    public Button restartButton;

    // Start is called before the first frame update
    void Start()
    {
        ballRb = gameObject.GetComponent<Rigidbody>();
        trajectoryLine = GetComponent<TrajectoryLine>();

        counter = GameObject.Find("Box").GetComponent<Counter>();

        restartButton.onClick.AddListener(RestartGame);

        isGameActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameActive)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                ResetBall();
            }

            if (Input.GetMouseButtonDown(0) && hasShot == false)
            {

                worldPointDown = GetMousePositionInWorld();

                Debug.Log("Mouse down: " + worldPointDown);
            }

            // Used to render the trajectory line
            if (Input.GetMouseButton(0) && hasShot == false)
            {
                Vector3 currentPoint = GetMousePositionInWorld();

                trajectoryLine.RenderLine(worldPointDown, currentPoint);
            }

            if (Input.GetMouseButtonUp(0) && hasShot == false)
            {
                worldPointUp = GetMousePositionInWorld();

                Debug.Log("Mouse up: " + worldPointUp);

                shotDirection = worldPointDown - worldPointUp;
                shotDirection.z = 0f;

                ballRb.AddForce(shotDirection * shotStrength, ForceMode.Impulse);

                hasShot = true;
                trajectoryLine.EndLine();
            }
        }
    }

    Vector3 GetMousePositionInWorld()
    {
        // the third parameter (z) is "distance from camera" - camera is at z = -30, we want z = 0, so -30 + 30.
        return Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 30f));
    }    

    void ResetBall()
    {
        // Reset the ball on the stand
        ballRb.velocity = Vector3.zero;
        ballRb.angularVelocity = Vector3.zero;
        transform.localPosition = Vector3.zero;

        // Only move the box if the player has scored
        if (counter.hasScored == true)
        {
            box.transform.position = new Vector3(Random.Range(-11, 22), 0, 0);
            counter.hasScored = false;
        }

        hasShot = false;
    }

    // Stop game, bring up game over text and restart button
    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        isGameActive = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
