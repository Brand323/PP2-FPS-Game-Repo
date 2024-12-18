using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class ClickMove : MonoBehaviour
{
    public Camera MapCamera;

    [Header("Movement Settings")]
    [SerializeField] float movespeed = 5f;

    [Header("Army Settings")]
    public int playerArmySize;
    [SerializeField] TextMeshProUGUI playerArmySizeText;


    //Stores current coroutine so i can interupt
    private Coroutine moveCourtine;

    bool entered;

    private void Start()
    {
        GameObject mapCameraObject = GameObject.FindGameObjectWithTag("MapCamera");

        if (mapCameraObject != null)
        {
            MapCamera = mapCameraObject.GetComponent<Camera>();
        }

        SetPlayerArmySize(1);

        Canvas canvas = GetComponentInChildren<Canvas>();

        canvas.worldCamera = MapCamera;

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = MapCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Map"))
                {
                    if(moveCourtine != null)
                    {
                        StopCoroutine(moveCourtine);
                    }

                   moveCourtine = StartCoroutine(MovePlayerOnMap(hit.point));
                }
            }
        }
    }

    private IEnumerator MovePlayerOnMap(Vector3 movePosition)
    {

        while (Vector3.Distance(transform.position, movePosition ) > 0.1f)
        {
            transform.position =  Vector3.MoveTowards(transform.position, movePosition, movespeed*Time.deltaTime);
        yield return null;
        }
        
        moveCourtine = null;
    }


    public void SetPlayerArmySize(int size)
    {
        playerArmySize = size;
       // Debug.Log("player Army Size set to: " + playerArmySize);
        UpdatePlayerArmySizeUI();
        CombatManager.instance.playerArmySize = size;
    }

    void UpdatePlayerArmySizeUI()
    {
        if (playerArmySizeText != null)
        {
            playerArmySizeText.text = playerArmySize.ToString();
         //   Debug.Log("Player Army Size Text Updated: " + playerArmySizeText.text);
        }
        else
        {
            Debug.Log("armySizeText is null");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "City" && !entered)
        {
            gameManager.instance.currentCity = other.gameObject;
            MapKingdomManager.instance.CurrentCity = other.transform;
            UIManager.instance.isPaused = !UIManager.instance.isPaused;
            if (MapKingdomManager.instance.IsCityInHumanKingdom(other.transform) == true)
            {
                UIManager.instance.PauseGame(UIManager.instance.cityMapWindow);
            }
            else
            {
                UIManager.instance.PauseGame(UIManager.instance.enemyCityMapWindow);
                MapKingdomManager.instance.findNearestCity();
                gameManager.instance.inBattleForCity = true;
            }
            entered = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "City")
        {
            entered = false;
        }
    }
}
