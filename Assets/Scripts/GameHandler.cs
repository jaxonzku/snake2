using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    [SerializeField] private Snake snake;




    private Food food;
    void Start()
    {
        food = new Food(33, 15);
        snake.SetUp(food, 1);
        food.SetUp(snake);

    }
}
