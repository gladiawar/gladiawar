using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * This class provide extensions methods to MonoBehaviour
 * 
 * @author Claude Ramseyer
 */
public static class GladiawarBehaviour {

	public static void getCurrentPlayer(this MonoBehaviour obj) {
		return GladiatorNetwork._myGladiator;
	}
	
	public static List<GladiatorNetwork> getPlayers(this MonoBehaviour obj) {
		return LogicInGame.Instance.PlayerList;
	}
	
}
