using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject birdPrefab;
    [SerializeField] GameObject scenerayPrefab;
    [SerializeField] GameObject sceneToBeDestroyed;
    [SerializeField] GameObject sceneNewlyInstantiated;

    [SerializeField] float scrollingSpeed = 40f;
    [SerializeField] float xPosToDestroyScenery = -12.5f;
    [SerializeField] float xPosToCreateScenery = -6.8f;
    [SerializeField] Vector3 newScenePosition;
    [SerializeField] Vector3 oldScenePosition;

    private void Awake()
    {
        Instantiate(birdPrefab, Vector3.zero, Quaternion.identity); //Instatiating bird at starting screen
        sceneNewlyInstantiated = Instantiate(scenerayPrefab, Vector3.zero, Quaternion.identity); //Instantiating Started Screen
        sceneToBeDestroyed = sceneNewlyInstantiated;    //At first both will point to same scenery
        sceneNewlyInstantiated.GetComponent<SceneryScroller>().setScrollingSpeed(scrollingSpeed);   //Setting speed of scrolling
    }

    private void Update()
    {
        newScenePosition = sceneNewlyInstantiated.GetComponent<Transform>().position;
        oldScenePosition = sceneToBeDestroyed.GetComponent<Transform>().position;

        checkCreateScenery();
        checkDestroyScenery();

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
        sceneNewlyInstantiated.GetComponent<SceneryScroller>().setScrollingSpeed(scrollingSpeed);
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

}
