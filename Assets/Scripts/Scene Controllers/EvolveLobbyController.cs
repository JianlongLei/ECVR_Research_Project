using System.Collections;
using RevolVR;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EvolveLobbyController : MonoBehaviour
{

    public GameObject simRunnerObject;
    public GameObject sceneLoaderObject;
    public Button startButton;
    public TMP_Text startButtonText;
    public GameObject headerObject;
    private AppConfig config;
    private string configPath;

    public void Start()
    {
        startButton.interactable = false;
        configPath = "Assets/revolve2/vr/db/config.json";
        config = ConfigManager.LoadConfig(configPath);
        config.GENERATION_INDEX++;
        ConfigManager.SaveConfig(configPath, config);
        StartCoroutine(RunSim());
    }

    private IEnumerator RunSim()
    {
        SimRunner simRunner = simRunnerObject.GetComponent<SimRunner>();
        if (simRunner == null)
        {
            Debug.LogError("SimRunner component is missing from the specified GameObject.");
            yield break;
        }
        simRunner.FileName = "evolve_robots.py";
        yield return StartCoroutine(simRunner.RunRevolveAsync());
        headerObject.GetComponent<TMP_Text>().text = "Simulation is ready!";
        startButtonText.text = "Start Simulation";
        startButton.interactable = true;
    }

    public void LoadScene()
    {
        StartCoroutine(LoadSceneCoroutine());
    }

    private IEnumerator LoadSceneCoroutine()
    {
        SceneLoader sceneLoader = sceneLoaderObject.GetComponent<SceneLoader>();
        if (sceneLoader == null)
        {
            Debug.LogError("SceneLoader component is missing from the specified GameObject.");
            yield break;
        }
        yield return sceneLoader.LoadSceneAsync();
    }
}
