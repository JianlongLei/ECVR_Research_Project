using System;
using UnityEngine;
using Mujoco;
using System.IO;
using RevolVR;
using System.Data;
using Mono.Data.Sqlite;
using System.Collections.Generic;

public class ArenaController : MonoBehaviour
{

	public GameObject simRunnerObject;

	public class Individual
	{
		public int Id { get; set; }
		public int PopulationId { get; set; }
		public int PopulationIndex { get; set; }
		public int GenotypeId { get; set; }
		public float Fitness { get; set; }

	}

	void Start()
	{
		Application.targetFrameRate = 100;
		Time.timeScale = 1;

		SimRunner simRunner = simRunnerObject.GetComponent<SimRunner>();
		if (simRunner == null)
		{
			Debug.LogError("SimRunner component is missing from the specified GameObject.");
			return;
		}

		GameObject mujocoScene1 = simRunner.ImportMujocoScene();
		// GameObject mujocoScene1 = simRunner.ImportMujocoSceneFromXML();

		simRunner.ApplyShaders();
		simRunner.AddInfoObjects();
		MjScene.Instance.ctrlCallback += (_, _) => simRunner.TrackMujocoData();

		string filePath = Path.Combine(Application.dataPath, "animation_data.json");
		filePath = filePath.Replace("\\", "/");
		string jsonData = File.ReadAllText(filePath);

		SimulationSceneState[] sceneStates = JsonHelper.FromJson<SimulationSceneState>(jsonData);
		simRunner.simulationScene = new SimulationScene { scenes = sceneStates };
	}
}
