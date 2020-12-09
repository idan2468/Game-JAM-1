using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
	
	[SerializeField]
	protected string m_PlayerName;

	protected virtual void Start ()
	{
		
	}

	public string GetPlayerName ()
	{
		return m_PlayerName;
	}
	
}
