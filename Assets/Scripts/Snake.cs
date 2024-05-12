using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;


public class Snake : MonoBehaviour
{
    // Start is called before the first frame update
    private float gridMoveTimerMax;
    private float gridMoveTimer;
    private Vector2Int gridPosition;
    private Vector3Int bodyRotation;
    private Food food;
    private int snakeBodyCount;
    public List<Vector2Int> snakeBodyPositions;
    private List<Vector2Int> snakeBodyWithFoodPositions;
    private List<Vector3Int> snakeBodyRotations;
    private List<GameObject> snakeBodyGameObjects;
    private int player;
    public ScoreController scoreController;

    private enum Direction
    {
        Left,
        Right,
        Up,
        Down
    }
    private Direction gridMoveDirection;
    SnakeBody snakeBody;
    private void Awake()
    {
        gridMoveTimerMax = .2f;
        gridMoveTimer = gridMoveTimerMax;
        snakeBodyCount = 1;
    }
    public void SetUp(Food food, int player)
    {
        this.food = food;
        this.player = player;

    }

    void Start()
    {
        food.spawnFoodOnScreen();

        snakeBodyPositions = new List<Vector2Int>(snakeBodyCount);
        snakeBodyRotations = new List<Vector3Int>(snakeBodyCount);
        snakeBodyPositions.Insert(snakeBodyCount - 1, new Vector2Int(0, 0));
        snakeBodyRotations.Insert(snakeBodyCount - 1, new Vector3Int(0, 0, 0));
        snakeBodyGameObjects = new List<GameObject>(snakeBodyCount);
        snakeBody = new SnakeBody();
        snakeBodyGameObjects.Insert(snakeBodyCount - 1, snakeBody.CreateABody(player));
    }
    void Update()
    {
        HandleGridMovement();
        HandleInput();
        BorderHit();
    }

    private void HandleGridMovement()
    {
        gridMoveTimer += Time.deltaTime;
        if (gridMoveTimer > gridMoveTimerMax)
        {
            gridMoveTimer -= gridMoveTimerMax;
            Vector2Int gridMoveDirectionVector;
            switch (gridMoveDirection)
            {
                default:
                case Direction.Right:
                    gridMoveDirectionVector = new Vector2Int(+1, 0);
                    bodyRotation = new Vector3Int(0, 180, 0);

                    break;
                case Direction.Left:
                    gridMoveDirectionVector = new Vector2Int(-1, 0);
                    bodyRotation = new Vector3Int(0, 0, 0);
                    break;

                case Direction.Up:
                    gridMoveDirectionVector = new Vector2Int(0, +1);
                    bodyRotation = new Vector3Int(0, 180, -90);
                    break;
                case Direction.Down:
                    gridMoveDirectionVector = new Vector2Int(0, -1);
                    bodyRotation = new Vector3Int(0, 0, 90);
                    break;
            }
            gridPosition += gridMoveDirectionVector;
            snakeBodyRotations[snakeBodyCount - 1] = bodyRotation;
            snakeBodyPositions[snakeBodyCount - 1] = gridPosition;
            HandleEachMove();
        }
        CheckFoodProximity(food.foodGridPosition);

        bool snakeAteFood = food.SnakeAtefood(gridPosition);
        if (snakeAteFood)
        {
            snakeBodyCount++;
            snakeBodyPositions.Add(new Vector2Int(0, 0));
            snakeBodyRotations.Add(new Vector3Int(0, 0, 0));
            snakeBodyGameObjects.Add(snakeBody.CreateABody(player));
            if (CheckSelfCollision())
            {
                SceneManager.LoadScene(0);
            }
            scoreController.IncrementScore(5);
        }

    }


    private void HandleEachMove()
    {
        for (int i = 0; i < snakeBodyCount; i++)
        {
            snakeBodyGameObjects[index: i].gameObject.transform.position = new Vector3(snakeBodyPositions[i].x, snakeBodyPositions[i].y);
            Vector3 rotationEulerAngles = snakeBodyRotations[i];
            Quaternion rotationQuaternion = Quaternion.Euler(rotationEulerAngles);
            snakeBodyGameObjects[i].transform.rotation = rotationQuaternion;
        }
        {
            for (int i = snakeBodyCount - 1; i > 0; i--)
            {
                snakeBodyGameObjects[i - 1].GetComponent<SpriteRenderer>().sprite = GameAssets.i.SnakeBody;
                snakeBodyPositions[i] = snakeBodyPositions[i - 1];
                snakeBodyRotations[i] = snakeBodyRotations[i - 1];
                snakeBodyGameObjects[i - 1] = snakeBodyGameObjects[i - 1];
            }
            snakeBodyPositions[0] = gridPosition;
            snakeBodyRotations[0] = bodyRotation;
        }
        if (CheckSelfCollision())
        {
            SceneManager.LoadScene(0);
        }
    }

    class SnakeBody
    {
        public GameObject CreateABody(int player)
        {
            GameObject newBody = new("snakebody" + player.ToString(), typeof(SpriteRenderer));
            newBody.transform.position = new Vector2(200, 200);
            Vector3 rotationEulerAngles = new Vector3(0, 0, 0);
            Quaternion rotationQuaternion = Quaternion.Euler(rotationEulerAngles);
            newBody.transform.rotation = rotationQuaternion;
            newBody.GetComponent<SpriteRenderer>().sprite = GameAssets.i.SnakeHead;
            return newBody;
        }
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (gridMoveDirection != Direction.Down)
            {
                gridMoveDirection = Direction.Up;
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (gridMoveDirection != Direction.Up)
            {
                gridMoveDirection = Direction.Down;
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (gridMoveDirection != Direction.Right)
            {
                gridMoveDirection = Direction.Left;
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (gridMoveDirection != Direction.Left)
            {
                gridMoveDirection = Direction.Right;
            }
        }
    }
    private bool CheckSelfCollision()
    {
        for (int i = 1; i < snakeBodyCount; i++)
        {
            if (snakeBodyPositions[i] == gridPosition)
            {
                return true;
            }
        }
        return false;
    }

    public void CheckFoodProximity(Vector2Int foodPosition)
    {
        float distance = Vector2Int.Distance(gridPosition, foodPosition);
        float thresholdDistance = 1; // You can adjust this value as needed
        if (distance <= thresholdDistance)
        {

            if (snakeBodyCount == 1)
            {
                snakeBodyGameObjects[index: 0].gameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.i.SnakeHeadOpen;

            }
            else
            {
                snakeBodyGameObjects[index: snakeBodyCount - 1].gameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.i.SnakeHeadOpen;
            }
        }
        else
        {
            snakeBodyGameObjects[index: snakeBodyCount - 1].gameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.i.SnakeHead;
        }

    }

    public void BorderHit()
    {


        if (snakeBodyPositions[0].x == 34)
        {
            gridPosition = new Vector2Int(-34, snakeBodyPositions[0].y);
        }
        if (snakeBodyPositions[0].x == -34)
        {
            gridPosition = new Vector2Int(34, snakeBodyPositions[0].y);
        }
        if (snakeBodyPositions[0].y == 15)
        {
            gridPosition = new Vector2Int(snakeBodyPositions[0].x, -18);
        }
        if (snakeBodyPositions[0].y == -18)
        {
            gridPosition = new Vector2Int(snakeBodyPositions[0].x, 15);
        }

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("boreder"))
        {
            Debug.Log("hottt hitt");
        }
    }
    public Vector2Int GetSnakePosition()
    {
        return gridPosition;
    }



}
