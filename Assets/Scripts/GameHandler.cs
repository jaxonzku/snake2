using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    [SerializeField] private Snake snake;
    private Food food;
    void Start()
    {
        // Debug.Log("handler runnning");
        food = new Food(33, 17);
        snake.SetUp(food);
        food.SetUp(snake);
    }
}
