using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum PlayerIndex
{
	Player1,
	Player2
}
public class GameManager : Singleton<GameManager>
{
	private GameObject treasure;
	private Animator treasureAnimator;
	private ParticleSystem winningEffect;
	private CinemachineVirtualCamera mainCamera;
	private Transform endCameraPosition;
	
	private void Awake()
	{
		treasure = GameObject.FindWithTag("Treasure");
		if (treasure == null)
		{
			Debug.LogWarning("GameManager Warning: No Treasure in Scene!");
			return;
		}

		treasureAnimator = treasure.GetComponent<Animator>();
		if (treasureAnimator == null) Debug.LogWarning("GameManager Warning: No Treasure Animation!");
		
		mainCamera = Camera.main.GetComponentInChildren<CinemachineVirtualCamera>();
		endCameraPosition = Resources.Load<Transform>("Camera End Position");
	}

	// #region Menus
	// [UnityEditor.MenuItem("Winning/Win Player1")]
	// public static void Win1()
	// {
	// 	GameManager.Instance.OnPlayerWin(PlayerIndex.Player1);
	// }
	// [UnityEditor.MenuItem("Winning/Win Player2")]
	// public static void Win2()
	// {
	// 	GameManager.Instance.OnPlayerWin(PlayerIndex.Player2);
	// }
	// #endregion
	
	
	public void OnPlayerWin(PlayerIndex p)
	{
		treasureAnimator?.SetTrigger("OpenTreasure");
		UIController.Instance.UpdatePlayerWon(p);
		Time.timeScale = .1f;

		mainCamera.LookAt = endCameraPosition;
		
		StartCoroutine(MoveToWinScene());
	}


	private IEnumerator MoveToWinScene()
	{
		yield return new WaitForSecondsRealtime(3.5f);
		Debug.Log("Loaded end scene");
		SceneLoader.Instance.moveToScene(SceneLoader.Scene.EndScene);
	}
}
