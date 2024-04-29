using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food 
{
    private Vector2Int foodGridPosition;
    private Snake snake;
    private int width;
    private int height;
    private GameObject foodGameObject;
    public Sprite squareTile;

    public Food(int width, int height)
    {
        this.width = width;
        this.height = height;
    }
    public void SetUp(Snake snake)
    {
        Debug.Log("setup running in food");

        this.snake = snake;
    }

   

    public void spawnFoodOnScreen()
    {
        
        foodGridPosition = new Vector2Int(Random.Range(0, width), Random.Range(0, height));
        foodGameObject = new GameObject("Food", typeof(SpriteRenderer));
        foodGameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.i.foodsprite;
        foodGameObject.transform.position = new Vector3(foodGridPosition.x, foodGridPosition.y);
        Debug.Log("spawing food"+foodGridPosition);
    }

    public bool SnakeAtefood(Vector2Int snakeGridPosition)
    {
        if (foodGridPosition == snakeGridPosition)
        {
            Debug.Log("snake Ate Food");
            Object.Destroy(foodGameObject);
            spawnFoodOnScreen();
            return true;
        }
        else
        {
            return false;
        }
    }
}
