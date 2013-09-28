/*
 * 
 * 
 * 
 */

using					UnityEngine;
using					System.Collections;
using					System.Collections.Generic;

public class 			DisplayPersoList : MonoBehaviour
{
	public GameObject		_panel;
	
	private Transform[] 	_slotPerso;	
	public PlayerInfoBaseBehaviour[]	_players;
	
	void 				Start()
	{
		_slotPerso = new Transform[3];
		for (int i = 0; i < 3; ++i)
		{
			_slotPerso[i] = transform.FindChild((i + 1).ToString());
			_slotPerso[i].GetComponent<UIButton>().isEnabled = false;
		}
	}
	
	public void			loadCharacters()
	{
		SDNet.Instance.NormalRequest(GamePHP.Instance.MainUrl + "listp/?PHPSESSID=" + RunTimeData.sessionID, OnCharactersLoaded, new Dictionary<string, string>() { { "u", "" } });
	}
	
	public void			OnCharactersLoaded(SDNet.ReturnCode code, string res)
	{
		if (code == SDNet.ReturnCode.OK)
		{
			if (res.Length > 0)
			{
				parseRequest(res);
				displayCharacter();
			}
		}
		else
			_panel.GetComponent<PanelLogin>().State = PanelLogin.ePanelLoginState.LOGIN;
	}
	
	private void			parseRequest(string res)
	{
		string			first = res.Substring(1);
		string[]		persos = first.Split(new char[] { '|' });
		int				i = 0;
		
		foreach (string perso in persos)
		{
			string[]	data = perso.Split(new char[] { '/' });
			
			_players[i].PlayerName = data[0];
			_players[i].PlayerClass = (SelectClass.eClass)int.Parse(data[1]);
			++i;
		}
		
	}
	
	private void 		displayCharacter()
	{
		for(int i = 0; i < _players.Length; ++i)
			if (_players[i].PlayerName.Length > 2)
			{
				_slotPerso[i].FindChild("Label").GetComponent<UILabel>().text = _players[i].PlayerName;
				_slotPerso[i].GetComponent<UIButton>().isEnabled = true;
			}
	}
	
	public void			OnClickSelectCharacterOne()
	{
		/*_playerinfo.SetPlayerName(_players[0].PlayerName);
		if (_playerinfo.GetPlayerName().Length > 0)
			_playerinfo.PlayerIsSet = true;
		else
			_playerinfo.PlayerIsSet = false;*/
	}
	
	public void			OnClickSelectCharacterTwo()
	{
/*		_playerinfo.SetPlayerName(_players[1].GetPlayerName());
		if (_playerinfo.GetPlayerName().Length > 0)
			_playerinfo.PlayerIsSet = true;
		else
			_playerinfo.PlayerIsSet = false;*/
	}
	
	public void			OnClickSelectCharacterThree()
	{
/*		_playerinfo.SetPlayerName(_players[2].GetPlayerName());
		if (_playerinfo.GetPlayerName().Length > 0)
			_playerinfo.PlayerIsSet = true;
		else
			_playerinfo.PlayerIsSet = false;*/
	}
	
	public void			OnClickLoadLobby()
	{
/*		if (_playerinfo.PlayerIsSet)
			Application.LoadLevel("ListePartie");
		else
		{
			//TODO: Afficher l'erreur
			Debug.LogError("Aucun joueur n'est selectionne");
		}*/
	}
}
