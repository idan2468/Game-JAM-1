﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using Cinemachine;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum PlayerIndex
{
	Player1,
	Player2
}
public class GameManager : Singleton<GameManager>
{
	[SerializeField] private GameObject treasure;
	[SerializeField] private GameObject endGameCanvas;
	[SerializeField] private float timeUntilWinning = 5;
	
	
	private Animator treasureAnimator;
	private Sprite[] endGameImages;
	private PlayerIndex winner;
	private Image imageComponent;

	private void Awake()
	{
		Time.timeScale = 1f;
		if (treasure == null)
		{
			Debug.LogWarning("GameManager Warning: No Treasure in Scene!");
			return;
		}

		treasureAnimator = treasure.GetComponent<Animator>();
		if (treasureAnimator == null) Debug.LogWarning("GameManager Warning: No Treasure Animation!");
		endGameImages = new[] {Resources.Load<Sprite>("Player1_Win"), Resources.Load<Sprite>("Player2_Win")};

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
		Time.timeScale = .1f;
		winner = p;
		StartCoroutine(MoveToWinScene());
		imageComponent = endGameCanvas.transform.GetChild(0).gameObject.GetComponent<Image>();
	}


	private IEnumerator MoveToWinScene()
	{
		yield return new WaitForSecondsRealtime(timeUntilWinning);
		endGameCanvas.SetActive(true);
		Time.timeScale = 0;
		imageComponent.sprite = endGameImages[(int)winner];
		
	}
}
