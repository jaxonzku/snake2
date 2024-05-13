using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food
{
    public Vector2Int foodGridPosition;
    private Snake snake;
    private int width;
    private int height;
    private GameObject foodGameObject;
    public GameObject creatureObject;
    private int foodsEaten = 0;
    private Vector2Int creatureGridPosition;
    public Sprite squareTile;
    private GameObject creatureGameObject;
    private float creatureSpawnInterval = 3f;
    private float creatureDestroyInterval = 5f;
    public Food(int width, int height)
    {
        this.width = width;
        this.height = height;
    }
    public void SetUp(Snake snake)
    {
        this.snake = snake;
    }
    public IEnumerator SpawnCreatureRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(creatureSpawnInterval);
            if (foodsEaten >= 3)
            {
                Debug.Log("starting coroutine");
                SpawnCreature();
                yield return new WaitForSeconds(creatureDestroyInterval);
                DestroyCreature();
                foodsEaten = 0;
            }
        }
    }
    private Vector2Int randomRangeGenerate()
    {
        Vector2Int randomLoc = new Vector2Int(Random.Range(0, width),
           Random.Range(0, height));
        if (!snake.snakeBodyPositions.Contains(randomLoc))
        {
            return randomLoc;
        }
        else
        {
            return randomRangeGenerate();
        }
    }

    public void spawnFoodOnScreen()
    {
        foodGridPosition = randomRangeGenerate();
        foodGameObject = new GameObject("Food", typeof(SpriteRenderer));
        foodGameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.i.foodsprite;
        foodGameObject.transform.position = new Vector3(foodGridPosition.x, foodGridPosition.y);
    }

    public bool SnakeAtefood(Vector2Int snakeGridPosition)
    {
        if (foodGridPosition == snakeGridPosition)
        {
            Object.Destroy(foodGameObject);
            foodsEaten++;
            spawnFoodOnScreen();
            return true;
        }
        else
        {
            return false;
        }
    }

    private void SpawnCreature()
    {
        creatureGridPosition = randomRangeGenerate();
        creatureGameObject = new GameObject("Creature", typeof(SpriteRenderer));
        creatureGameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.i.Creature; // Use the same sprite as food for now
        creatureGameObject.transform.position = new Vector3(creatureGridPosition.x, creatureGridPosition.y);
    }

    public void DestroyCreature()
    {
        if (creatureGameObject != null)
        {
            Object.Destroy(creatureGameObject);
        }
    }


    public bool SnakeAteCreature(Vector2Int snakeGridPosition)
    {
        if (creatureGameObject != null && creatureGridPosition == snakeGridPosition)
        {
            Object.Destroy(creatureGameObject);
            return true;
        }
        else
        {
            return false;
        }
    }


}
