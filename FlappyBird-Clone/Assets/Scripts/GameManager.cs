using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject birdPrefab;
    [SerializeField] GameObject pipePrefab;
    [SerializeField] GameObject scenerayPrefab;
    [SerializeField] GameObject sceneToBeDestroyed;
    [SerializeField] GameObject sceneNewlyInstantiated;

    [SerializeField] float pipeScrollingSpeed = 50f;
    [SerializeField] float minPipeHeight = -1.6f;
    [SerializeField] float maxPipeHeight = 3.7f;
    [SerializeField] float xPosToSpawnPipe = 3.8f;

    [SerializeField] float currentTimer = 0.0f;
    [SerializeField] float timeToSpawnPipe = 5.0f;

    [SerializeField] float sceneryScrollingSpeed = 40f;
    [SerializeField] float xPosToDestroyScenery = -12.5f;
    [SerializeField] float xPosToCreateScenery = -6.8f;

    [SerializeField] Vector3 newScenePosition;
    [SerializeField] Vector3 oldScenePosition;

    private void Awake()
    {
        Instantiate(birdPrefab, Vector3.zero, Quaternion.identity); //Instatiating bird at starting screen
        sceneNewlyInstantiated = Instantiate(scenerayPrefab, Vector3.zero, Quaternion.identity); //Instantiating Started Screen
        sceneToBeDestroyed = sceneNewlyInstantiated;    //At first both will point to same scenery
        sceneNewlyInstantiated.GetComponent<Scroller>().setScrollingSpeed(sceneryScrollingSpeed);   //Setting speed of scrolling
        spawnPipe();
    }

    private void Update()
    {
        newScenePosition = sceneNewlyInstantiated.GetComponent<Transform>().position;
        oldScenePosition = sceneToBeDestroyed.GetComponent<Transform>().position;

        checkCreateScenery();
        checkDestroyScenery();

        //Timer
        currentTimer += Time.deltaTime;
        checkSpawnPipe();

    }

    private void checkCreateScenery()
    {
        if(newScenePosition.x <= xPosToCreateScenery)
        {
            //Create new scenery at x = 12
            createNewScenery();
        }
    }

    private void createNewScenery()
    {
        sceneToBeDestroyed = sceneNewlyInstantiated;
        sceneNewlyInstantiated = Instantiate(scenerayPrefab, new Vector2(12, 0), Quaternion.identity);
        sceneNewlyInstantiated.GetComponent<Scroller>().setScrollingSpeed(sceneryScrollingSpeed);
    }

    private void checkDestroyScenery()
    {
        if(oldScenePosition.x <= xPosToDestroyScenery)
        {
            //Destroy scenery at x = -12.5f
            Destroy(sceneToBeDestroyed);
            sceneToBeDestroyed = sceneNewlyInstantiated;
        }
    }

    private void checkSpawnPipe()
    {
        if (currentTimer >= timeToSpawnPipe)
        {
            spawnPipe();
            currentTimer = 0.0f;
        }
    }

    private void spawnPipe()
    {
        GameObject temp = Instantiate(pipePrefab, new Vector2(xPosToSpawnPipe, Random.Range(minPipeHeight, maxPipeHeight)), Quaternion.identity);
        temp.GetComponent<Scroller>().setScrollingSpeed(pipeScrollingSpeed);
        Destroy(temp, 15);
    }

}
