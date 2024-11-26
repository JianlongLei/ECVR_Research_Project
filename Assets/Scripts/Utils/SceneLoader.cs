using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

using Debug = UnityEngine.Debug;

namespace RevolVR {

	public class SceneLoader : MonoBehaviour
	{
		public string sceneName;

		public IEnumerator LoadSceneAsync()
		{
			AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);

			asyncLoad.allowSceneActivation = false;

			while (!asyncLoad.isDone)
			{
				if (asyncLoad.progress >= 0.9f)
				{
					asyncLoad.allowSceneActivation = true;
				}

				yield return null;
			}
		}
	}
}
