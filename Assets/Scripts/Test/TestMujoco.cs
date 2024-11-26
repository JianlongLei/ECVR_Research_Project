using System;
using UnityEngine;
using Mujoco;
using System.IO;
using RevolVR;
using System.Data;
using Mono.Data.Sqlite;
using System.Collections.Generic;
using Unity.VisualScripting;

public class TestMujocoController : MjScene
{
    public GameObject simRunnerObject;
    public GameObject brainPrefab;

    private float timeCounter = 0.0f;
    private float timeCounterUpdate = 0.0f;
	private const double targetFrameRate = 1000.0f;
    SimulationSceneState[] sceneStates;
    private int currentIndex = 0;

    public unsafe GameObject ImportMujocoScene()
    {
        string path = $"{Application.dataPath}/model.xml";
        var importer = new MjImporterWithAssets();
        Model = MjEngineTool.LoadModelFromFile(path);
        Data = MujocoLib.mj_makeData(Model);
        GameObject importedScene = null;
        while (importedScene == null)
        {
            importedScene = importer.ImportFile(path);
        }
        importedScene.tag = "MuJoCoImport";
        return importedScene;
    }

    public void ApplyShaders()
    {
        Shader newShader = Shader.Find("Universal Render Pipeline/Lit");

        GameObject[] mujocoObjects = GameObject.FindGameObjectsWithTag("MuJoCoImport");

        foreach (GameObject obj in mujocoObjects)
        {
            Renderer[] renderers = obj.GetComponentsInChildren<Renderer>();

            foreach (Renderer r in renderers)
            {
                foreach (Material m in r.materials)
                {
                    m.shader = newShader;
                }
            }
        }
    }

    public void AddInfoObjects()
    {
        GameObject mujocoScene = GameObject.FindWithTag("MuJoCoImport");
        if (mujocoScene == null)
        {
            Debug.Log("Couldn't find imported MuJoCo scene");
            return;
        }

        Transform sceneTransform = mujocoScene.transform;
        int i = 0;
        List<IndividualData> latestIndividuals = DatabaseManager.GetIndividualsDataFromLatestGeneration();
        foreach (Transform child in sceneTransform)
        {
            GameObject childObject = child.gameObject;
            string childName = childObject.name;
            if (childName.StartsWith("mbs") && childName != "mbs0/")
            {
                GameObject instantiatedPrefab = Instantiate(brainPrefab);
                RobotInfo robotInfo = instantiatedPrefab.GetComponent<RobotInfo>();
                if (robotInfo != null)
                {
                    if (i >= latestIndividuals.Count)
                    {
                        Debug.Log("There is a mismatch between the amount of users in the scene and the amount of individuals in the latest generation.");
                    }
                    else
                    {
                        IndividualData currentIndividual = latestIndividuals[i];
                        i++;
                        robotInfo.UpdateRobotInfo($"Robot {i}", currentIndividual.Fitness, currentIndividual.Id);
                    }
                }
                else
                {
                    Debug.LogError("RobotInfo script not found on the instantiated prefab.");
                }
                instantiatedPrefab.transform.SetParent(childObject.transform.GetChild(1), false);
                instantiatedPrefab.transform.position = childObject.transform.GetChild(1).position;
            }
        }
    }


    // void Start() {
    //     ImportMujocoScene();
    //     ApplyShaders();
    //     // AddInfoObjects();
    //     base.Start();
    // }
    unsafe void Start()
	{
        ImportMujocoScene();
        ApplyShaders();
		Application.targetFrameRate = 144;
		Time.timeScale = 1;

		// SimRunner simRunner = simRunnerObject.GetComponent<SimRunner>();
		// if (simRunner == null)
		// {
		// 	Debug.LogError("SimRunner component is missing from the specified GameObject.");
		// 	return;
		// }

		// GameObject mujocoScene1 = simRunner.ImportMujocoScene();

		// simRunner.ApplyShaders();
		// simRunner.AddInfoObjects();
		ctrlCallback += (_, _) => TrackMujocoData();

		string filePath = Path.Combine(Application.dataPath, "animation_data.json");
		filePath = filePath.Replace("\\", "/");
		string jsonData = File.ReadAllText(filePath);

		sceneStates = JsonHelper.FromJson<SimulationSceneState>(jsonData);

        base.Start();
	}

    protected unsafe void FixedUpdate()
    {
        timeCounterUpdate += Time.deltaTime;
        if (timeCounterUpdate < 1.0f / targetFrameRate)
        {
            return;
        }
        timeCounterUpdate = 0;
        base.FixedUpdate();
    }
    
    [System.Serializable]
    public class SimulationScene
    {
        public SimulationSceneState[] scenes;
    }

    unsafe public void TrackMujocoData()
    {
        timeCounter += Time.deltaTime;
        if (timeCounter < 1.0f / targetFrameRate)
        {
            return;
        }
        timeCounter = 0;
        if (currentIndex < sceneStates.Length)
        {

            for (int i = 0; i < sceneStates[currentIndex].xpos.Length; i++)
            {
                Data->xpos[i] = sceneStates[currentIndex].xpos[i];
            }

            for (int i = 0; i < sceneStates[currentIndex].xquat.Length; i++)
            {
                Data->xquat[i] = sceneStates[currentIndex].xquat[i];
            }

            for (int i = 0; i < sceneStates[currentIndex].qpos.Length; i++)
            {
                Data->qpos[i] = sceneStates[currentIndex].qpos[i];
            }
        }

        currentIndex++;
    }

    // void LateUpdate() {
    //     base.LateUpdate();
    //     ApplyShaders();
    // }

    // unsafe void Update() {
    //     MujocoLib.mj_step(Model, Data);
    //     string text2 = Path.Combine(Application.temporaryCachePath, "temp" + ".xml");
    //     MjEngineTool.SaveModelToFile(text2, Model);
    //     string mjcfString = File.ReadAllText(text2);
    // }
}