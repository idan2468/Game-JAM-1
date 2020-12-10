using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum PlayerIndex
{
	Player1,
	Player2
}
public class GameManager : Singleton<GameManager>
{

	public void OnPlayerWin(PlayerIndex p)
	{
		Debug.Log(p.ToString() + " Won!!");
		Time.timeScale = 0;
		StartCoroutine(MoveToWinScene());
	}


	private IEnumerator MoveToWinScene()
	{
		yield return new WaitForSeconds(2);
		SceneLoader.Instance.moveToScene(SceneLoader.Scene.EndScene);
	}
}
