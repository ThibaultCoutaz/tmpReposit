using UnityEngine;
using System.Collections;

public class BaseBehaviour : MonoBehaviour {

    public TextMesh textScore;
    public TeamScript.Team team;

    public int nbShield = 3;
    private int currentShield = 1;
    public float[] lifePerShield;
    public float BaseLife;
    public float incrementLife;

	void Start () {
        switch (team)
        {
            case TeamScript.Team.red:
                textScore.color = Color.red;
                break;
            case TeamScript.Team.blue:
                textScore.color = Color.blue;
                break;
            default:
                textScore.color = Color.white;
                Debug.LogError("there is no team attach to this team !");
                break;
        }

        lifePerShield = new float[nbShield];
        lifePerShield[0] = BaseLife;
        for(int i = 1; i < nbShield; i++)
        {
            lifePerShield[i] = BaseLife + i * incrementLife;
        }

        textScore.text = "Life = " + lifePerShield[0] + "\n Shield = " + nbShield;
	}

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.GetComponent<BallBehaviour>())
        {
            BallBehaviour ballScript = col.gameObject.GetComponent<BallBehaviour>();
            if (ballScript.state != BallBehaviour.stateBall.Catch && PhotonView.Find(ballScript.IDSender).GetComponent<PhotonView>().owner.GetPlayerTeam() != team)
            {
                Debug.LogError(PhotonView.Find(ballScript.IDSender).GetComponent<PlayerScript>().caracterisiticCurrent.Attaque);
                lifePerShield[currentShield - 1] -= PhotonView.Find(ballScript.IDSender).GetComponent<PlayerScript>().caracterisiticCurrent.Attaque;
                if(lifePerShield[currentShield - 1] <= 0)
                {
                    currentShield ++;
                }
                textScore.text = "Life = " + lifePerShield[currentShield-1] + "\n Shield = " + (nbShield- (currentShield-1));
            }
        }
    }

}
