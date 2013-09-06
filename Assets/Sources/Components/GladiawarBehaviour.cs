using UnityEngine;
using System.Collections;

<<<<<<< HEAD
public class GladiawarBehaviour : MonoBehaviour {
=======
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
>>>>>>> 3466a78... Implement getPlayers/getCurrentPlayer in GladiawarBehavior
	
}
