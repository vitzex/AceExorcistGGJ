using UnityEngine;
using System.Collections;

public class MainGameLoop : MonoBehaviour {

	public AceExorcistGame AEG;//This has to be static, doesn't it? 
	//Or I can just add it to the camera as well(can't, would have to derive from monoBehavior


	// Use this for initialization
	void Start () {
		AEG.IsExorcistTurn=false;//the game always starts with the summoner. Deal with it
	}
	
	// Update is called once per frame
	void Update () {
		if(AEG.IsExorcistTurn)
		{
			if(this.gameObject.GetComponent<Player_Turn>()==null)//exorcist's turn has started, must add its script
			{
				this.gameObject.AddComponent<Player_Turn>();
			}
		}
		else//it's the summoner's turn
		{
			if(this.gameObject.GetComponent<Enemy_Turn>()==null)
			{
				this.gameObject.AddComponent<Enemy_Turn>();//summoner's turn has started
			}
		}
	}
}
