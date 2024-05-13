using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;


public class Snake2 : MonoBehaviour
{
    // Start is called before the first frame update
    private float gridMoveTimerMax;
    private float gridMoveTimer;
    private Vector2Int gridPosition;
    private Vector3Int bodyRotation;
    private Food2 food2;
    private int snakeBodyCount;
    public List<Vector2Int> snakeBodyPositions;

    private List<Vector3Int> snakeBodyRotations;
    private List<GameObject> snakeBodyGameObjects;
    private Direction gridMoveDirection;

    private int player;

    private float gridMoveTimerMax2;
    private float gridMoveTimer2;
    private Vector2Int gridPosition2;
    private Vector3Int bodyRotation2;
    private int snakeBodyCount2;
    public List<Vector2Int> snakeBodyPositions2;
    private List<Vector3Int> snakeBodyRotations2;
    private List<GameObject> snakeBodyGameObjects2;
    private Direction gridMoveDirection2;

    public ScoreController scoreController;

    private enum Direction
    {
        Left,
        Right,
        Up,
        Down
    }

    SnakeBody snakeBody;
    SnakeBody snakeBody2;

    private void Awake()
    {
        gridMoveTimerMax = .2f;
        gridMoveTimer = gridMoveTimerMax;
        snakeBodyCount = 1;
        gridMoveTimerMax2 = .2f;
        gridMoveTimer2 = gridMoveTimerMax;
        snakeBodyCount2 = 1;
    }
    public void SetUp(Food2 food2, int player)
    {
        this.food2 = food2;
        this.player = player;

    }
    void Start()
    {
        food2.spawnFoodOnScreen();
        StartCoroutine(food2.SpawnCreatureRoutine());
        snakeBodyPositions = new List<Vector2Int>(snakeBodyCount);
        snakeBodyRotations = new List<Vector3Int>(snakeBodyCount);
        snakeBodyPositions.Insert(snakeBodyCount - 1, new Vector2Int(0, 0));
        snakeBodyRotations.Insert(snakeBodyCount - 1, new Vector3Int(0, 0, 0));
        snakeBodyGameObjects = new List<GameObject>(snakeBodyCount);
        snakeBody = new SnakeBody();
        snakeBodyGameObjects.Insert(snakeBodyCount - 1, snakeBody.CreateABody(1));



        snakeBodyPositions2 = new List<Vector2Int>(snakeBodyCount2);
        snakeBodyRotations2 = new List<Vector3Int>(snakeBodyCount2);
        snakeBodyPositions2.Insert(snakeBodyCount2 - 1, new Vector2Int(0, 0));
        snakeBodyRotations2.Insert(snakeBodyCount2 - 1, new Vector3Int(0, 0, 0));
        snakeBodyGameObjects2 = new List<GameObject>(snakeBodyCount2);
        snakeBody2 = new SnakeBody();
        snakeBodyGameObjects2.Insert(snakeBodyCount2 - 1, snakeBody2.CreateABody(2));
    }
    void Update()
    {
        HandleGridMovement();
        HandleInput();
        BorderHit(2);
        BorderHit(2);
        HandleGridMovement2();
        HandleInput2();

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
        CheckFoodProximity(food2.foodGridPosition, 1);

        bool snakeAteFood = food2.SnakeAtefood(gridPosition);
        bool snakeAteCreature = food2.SnakeAteCreature(gridPosition);
        if (snakeAteFood || snakeAteCreature)
        {
            snakeBodyCount++;
            snakeBodyPositions.Add(new Vector2Int(0, 0));
            snakeBodyRotations.Add(new Vector3Int(0, 0, 0));
            snakeBodyGameObjects.Add(snakeBody.CreateABody(player));
            if (CheckSelfCollision(1))
            {
                SceneManager.LoadScene(0);
            }
            scoreController.IncrementScore(snakeAteFood ? 5 : 10);
        }
    }
    private void HandleGridMovement2()
    {
        gridMoveTimer2 += Time.deltaTime;
        if (gridMoveTimer2 > gridMoveTimerMax2)
        {
            gridMoveTimer2 -= gridMoveTimerMax2;
            Vector2Int gridMoveDirectionVector;
            switch (gridMoveDirection2)
            {
                default:
                case Direction.Right:
                    gridMoveDirectionVector = new Vector2Int(+1, 0);
                    bodyRotation2 = new Vector3Int(0, 180, 0);

                    break;
                case Direction.Left:
                    gridMoveDirectionVector = new Vector2Int(-1, 0);
                    bodyRotation2 = new Vector3Int(0, 0, 0);
                    break;

                case Direction.Up:
                    gridMoveDirectionVector = new Vector2Int(0, +1);
                    bodyRotation2 = new Vector3Int(0, 180, -90);
                    break;
                case Direction.Down:
                    gridMoveDirectionVector = new Vector2Int(0, -1);
                    bodyRotation2 = new Vector3Int(0, 0, 90);
                    break;
            }
            gridPosition2 += gridMoveDirectionVector;
            snakeBodyRotations2[snakeBodyCount2 - 1] = bodyRotation2;
            snakeBodyPositions2[snakeBodyCount2 - 1] = gridPosition2;
            HandleEachMove2();
        }
        CheckFoodProximity(food2.foodGridPosition, 2);

        bool snakeAteFood = food2.SnakeAtefood(gridPosition2);
        bool snakeAteCreature = food2.SnakeAteCreature(gridPosition2);
        if (snakeAteFood || snakeAteCreature)
        {
            snakeBodyCount2++;
            snakeBodyPositions2.Add(new Vector2Int(0, 0));
            snakeBodyRotations2.Add(new Vector3Int(0, 0, 0));
            snakeBodyGameObjects2.Add(snakeBody2.CreateABody(player));
            if (CheckSelfCollision(2))
            {
                SceneManager.LoadScene(0);
            }
            scoreController.IncrementScore(snakeAteFood ? 5 : 10);
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
        if (CheckSelfCollision(1))
        {
            SceneManager.LoadScene(0);
        }
    }
    private void HandleEachMove2()
    {
        for (int i = 0; i < snakeBodyCount2; i++)
        {
            snakeBodyGameObjects2[index: i].gameObject.transform.position = new Vector3(snakeBodyPositions2[i].x, snakeBodyPositions2[i].y);
            Vector3 rotationEulerAngles = snakeBodyRotations2[i];
            Quaternion rotationQuaternion = Quaternion.Euler(rotationEulerAngles);
            snakeBodyGameObjects2[i].transform.rotation = rotationQuaternion;
        }
        {
            for (int i = snakeBodyCount2 - 1; i > 0; i--)
            {
                snakeBodyGameObjects2[i - 1].GetComponent<SpriteRenderer>().sprite = GameAssets.i.SnakeBody;
                snakeBodyPositions2[i] = snakeBodyPositions2[i - 1];
                snakeBodyRotations2[i] = snakeBodyRotations2[i - 1];
                snakeBodyGameObjects2[i - 1] = snakeBodyGameObjects2[i - 1];
            }
            snakeBodyPositions2[0] = gridPosition2;
            snakeBodyRotations2[0] = bodyRotation2;
        }
        if (CheckSelfCollision(2))
        {
            SceneManager.LoadScene(0);
        }
    }
    class SnakeBody
    {
        public GameObject CreateABody(int player)
        {
            if (player == 1)
            {
                GameObject newBody = new("snakebody" + player.ToString(), typeof(SpriteRenderer));
                newBody.transform.position = new Vector2(200, 200);
                Vector3 rotationEulerAngles = new Vector3(0, 0, 0);
                Quaternion rotationQuaternion = Quaternion.Euler(rotationEulerAngles);
                newBody.transform.rotation = rotationQuaternion;
                newBody.GetComponent<SpriteRenderer>().sprite = GameAssets.i.SnakeHead;
                return newBody;

            }
            else
            {
                GameObject newBody = new("snakebody2" + player.ToString(), typeof(SpriteRenderer));
                newBody.transform.position = new Vector2(200, 200);
                Vector3 rotationEulerAngles = new Vector3(0, 0, 0);
                Quaternion rotationQuaternion = Quaternion.Euler(rotationEulerAngles);
                newBody.transform.rotation = rotationQuaternion;
                newBody.GetComponent<SpriteRenderer>().sprite = GameAssets.i.SnakeHead;
                return newBody;

            }



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
    private void HandleInput2()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (gridMoveDirection2 != Direction.Down)
            {
                gridMoveDirection2 = Direction.Up;
            }
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (gridMoveDirection2 != Direction.Up)
            {
                gridMoveDirection2 = Direction.Down;
            }
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (gridMoveDirection2 != Direction.Right)
            {
                gridMoveDirection2 = Direction.Left;
            }
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (gridMoveDirection2 != Direction.Left)
            {
                gridMoveDirection2 = Direction.Right;
            }
        }
    }
    private bool CheckSelfCollision(int player)
    {
        if (player == 1)
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
        else
        {
            for (int i = 1; i < snakeBodyCount2; i++)
            {
                if (snakeBodyPositions2[i] == gridPosition2)
                {
                    return true;
                }
            }
            return false;

        }

    }
    public void CheckFoodProximity(Vector2Int foodPosition, int player)
    {
        if (player == 1)
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
        float distance2 = Vector2Int.Distance(gridPosition2, foodPosition);
        float thresholdDistance2 = 1; // You can adjust this value as needed
        if (distance2 <= thresholdDistance2)
        {

            if (snakeBodyCount2 == 1)
            {
                snakeBodyGameObjects2[index: 0].gameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.i.SnakeHeadOpen;

            }
            else
            {
                snakeBodyGameObjects2[index: snakeBodyCount2 - 1].gameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.i.SnakeHeadOpen;
            }
        }
        else
        {
            snakeBodyGameObjects2[index: snakeBodyCount2 - 1].gameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.i.SnakeHead;
        }

    }
    public void BorderHit(int player)
    {
        if (player == 1)
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
        else
        {
            if (snakeBodyPositions2[0].x == 34)
            {
                gridPosition2 = new Vector2Int(-34, snakeBodyPositions2[0].y);
            }
            if (snakeBodyPositions2[0].x == -34)
            {
                gridPosition2 = new Vector2Int(34, snakeBodyPositions2[0].y);
            }
            if (snakeBodyPositions2[0].y == 15)
            {
                gridPosition2 = new Vector2Int(snakeBodyPositions2[0].x, -18);
            }
            if (snakeBodyPositions2[0].y == -18)
            {
                gridPosition2 = new Vector2Int(snakeBodyPositions2[0].x, 15);
            }
        }



    }




}
