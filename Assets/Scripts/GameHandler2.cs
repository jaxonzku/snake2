using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler2 : MonoBehaviour
{
    [SerializeField] private Snake2 snake2;




    private Food2 food2;
    void Start()
    {
        food2 = new Food2(33, 15);
        snake2.SetUp(food2, 1);
        food2.SetUp(snake2);

    }
}
