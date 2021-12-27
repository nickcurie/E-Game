using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject PauseUI;
    [SerializeField] private GameObject minimapCamera;
    [SerializeField] private GameObject[] spawnLocations;
    [SerializeField] private GameObject[] powerUpPrefabs;

    public bool isPaused;

    private static float powerUpSpawnCooldown = 10.0f;
    private float currentCooldown = powerUpSpawnCooldown;
    private int spawnLocationIdx;
    private int prefabIdx;
    private GameObject powerUpPrefab;
    private GameObject spawnLocation;

    // void Awake(){
    //     DontDestroyOnLoad(gameObject);
    // }

    private void Update()
    {
        if (Input.GetKeyDown("x"))
        {
            bool isActive = minimapCamera.activeSelf;
            minimapCamera.SetActive(!isActive);
        }
        if (Input.GetKeyDown("m"))
        {
            Debug.Log(PlayerData.ownsIceBlast);
            Debug.Log(PlayerData.ownsDrone);
            Debug.Log(PlayerData.ownsRocket);
        }
        if (Input.GetButtonDown("Cancel"))
        {
            isPaused = !isPaused;
            PauseUI.SetActive(!PauseUI.activeSelf);
            if (isPaused)
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }
            //SceneManager.LoadScene("Menu");
        }
    }
    private void FixedUpdate()
    {
        currentCooldown -= Time.fixedDeltaTime;

        if (currentCooldown < 0.0f && spawnLocations.Length > 0)
        {
            SpawnPowerUp();
            currentCooldown = powerUpSpawnCooldown;
        }
    }

    private void SpawnPowerUp()
    {
        spawnLocationIdx = Random.Range(0, spawnLocations.Length);
        prefabIdx = Random.Range(0, powerUpPrefabs.Length);

        powerUpPrefab = powerUpPrefabs[prefabIdx];
        spawnLocation = spawnLocations[spawnLocationIdx];

        Instantiate(powerUpPrefab, spawnLocation.transform);
    }

}
