using UnityEngine;
using UnityEngine.UI;
using TMPro; // Include this if using TextMeshPro
using RevolVR;
using System.Collections;

public class WristUIController : MonoBehaviour
{
    public GameObject selectParentsPrefab;
    public GameObject gameOverPrefab;
    private int offspringSize;
    private GameObject[] menuInstances;
    private AppConfig config;
    public GameObject sceneLoaderObject;
    public GameObject lobbyLoaderObject;

    void Start()
    {
        config = ConfigManager.LoadConfig("Assets/revolve2/vr/db/config.json");
        offspringSize = config.OFFSPRING_SIZE;
        menuInstances = new GameObject[offspringSize];
        if (config.GENERATION_INDEX <= config.NUM_GENERATIONS)
        {
            for (int i = 0; i < offspringSize; i++)
            {
                GameObject instance = Instantiate(selectParentsPrefab, this.transform, false);
                instance.name = "Menu " + (i + 1);
                menuInstances[i] = instance;

                // Set up button listeners
                Button nextButton = instance.transform.Find("NextButton").GetComponent<Button>();
                Button prevButton = instance.transform.Find("PreviousButton").GetComponent<Button>();

                TMP_Text header = instance.transform.Find("Header").GetComponent<TMP_Text>();
                header.text = $"Select parents of child {i + 1}/{offspringSize}";
                TMP_Text roundsText = instance.transform.Find("GenerationText").GetComponent<TMP_Text>();
                roundsText.text = $"Generation {config.GENERATION_INDEX}/{config.NUM_GENERATIONS}";

                if (i == 0)
                {
                    prevButton.interactable = false; // Disable previous button on the first prefab
                }
                if (i == offspringSize - 1)
                {
                    nextButton.GetComponentInChildren<TextMeshProUGUI>().text = "Submit"; // Change Next to Submit on the last prefab
                }

                nextButton.onClick.AddListener(() => ShowNextMenu(instance));
                prevButton.onClick.AddListener(() => ShowPreviousMenu(instance));

                // Initially set all inactive except the first one
                if (i > 0) instance.SetActive(false);
            }
        }
        else
        {
            GameObject instance = Instantiate(gameOverPrefab, this.transform, false);
            Button lobbyButton = instance.transform.Find("LobbyButton").GetComponent<Button>();
            lobbyButton.onClick.AddListener(() => StartCoroutine(BackToLobby()));
        }
    }

    private void ShowNextMenu(GameObject currentMenu)
    {
        TMP_Dropdown selectParent1 = currentMenu.transform.Find("SelectParent1").GetComponent<TMP_Dropdown>();
        string parent1Value = selectParent1.options[selectParent1.value].text;
        TMP_Dropdown selectParent2 = currentMenu.transform.Find("SelectParent2").GetComponent<TMP_Dropdown>();
        string parent2Value = selectParent2.options[selectParent2.value].text;
        if (parent1Value == "Select parent..." || parent2Value == "Select parent...")
        {
            TMP_Text selectParentError = currentMenu.transform.Find("SelectParentError").GetComponent<TMP_Text>();
            selectParentError.gameObject.SetActive(true);
            return;
        }
        int currentIndex = System.Array.IndexOf(menuInstances, currentMenu);
        if (currentIndex < offspringSize - 1)
        {
            menuInstances[currentIndex + 1].SetActive(true);
            currentMenu.SetActive(false);
        }
        else
        {
            Button nextButton = currentMenu.transform.Find("NextButton").GetComponent<Button>();
            nextButton.interactable = false;
            StartCoroutine(OnSubmit());
        }
    }

    private void ShowPreviousMenu(GameObject currentMenu)
    {
        int currentIndex = System.Array.IndexOf(menuInstances, currentMenu);
        if (currentIndex > 0)
        {
            menuInstances[currentIndex - 1].SetActive(true);
            currentMenu.SetActive(false);
        }
    }

    private IEnumerator BackToLobby()
    {
        SceneLoader lobbyLoader = lobbyLoaderObject.GetComponent<SceneLoader>();
        if (lobbyLoader == null)
        {
            Debug.LogError("LobbyLoader component is missing from the specified GameObject.");
            yield break;
        }
        yield return StartCoroutine(lobbyLoader.LoadSceneAsync());
    }

    public IEnumerator OnSubmit()
    {
        foreach (GameObject menu in menuInstances)
        {
            SelectParentsWristMenuController menuController = menu.GetComponent<SelectParentsWristMenuController>();
            if (menuController != null)
            {
                var (parent1Id, parent2Id, mutate) = menuController.GetSelectionData();
                int generationId = DatabaseManager.GetLatestGenerationId();
                if (generationId == -1)
                {
                    Debug.LogError("No generations found in the database.");
                    yield break;
                }
                bool submitted = DatabaseManager.InsertParents(parent1Id, parent2Id, generationId, mutate);
                if (!submitted)
                {
                    Debug.LogError("Could not update parents table.");
                    yield break;
                }
            }
            else
            {
                Debug.LogError("SelectParentsWristMenuController not found.");
                yield break;
            }
        }
        SceneLoader sceneLoader = sceneLoaderObject.GetComponent<SceneLoader>();
        if (sceneLoader == null)
        {
            Debug.LogError("SceneLoader component is missing from the specified GameObject.");
            yield break;
        }
        yield return StartCoroutine(sceneLoader.LoadSceneAsync());
    }
}
