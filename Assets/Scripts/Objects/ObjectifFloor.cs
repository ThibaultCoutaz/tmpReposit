using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class ObjectifFloor : MonoBehaviour
{

    public float timeToCatch = 10.0f;
    private float currentTimeToCatch = 0;

    public float timeImmunity = 10.0f;
    private float currentTimeImmunity = 0;

    public float amountOfGold = 10f;

    [SerializeField]
    private TextMesh timer;
    [SerializeField]
    private TextMesh immunityText;

    private TeamScript.Team teamInside;
    private int[] listPlayers;

    private bool objectifCatch = false;
    private bool immunity = false;
    private bool canImmunity = true;

    // Use this for initialization
    void Start()
    {
        teamInside = TeamScript.Team.none;
        timer.color = Color.green;

        //THis is a but hugly but nvm ( enfaite pas sur a voir)
        listPlayers = new int[2];
        listPlayers[0] = 0; //team red => 0 menber
        listPlayers[1] = 0; //team blue => 0 menber
    }

    // Update is called once per frame
    void Update()
    {
        //To manage the Immunity
        if(immunity && currentTimeImmunity < timeImmunity)
        {
            currentTimeImmunity += Time.deltaTime;
            if(currentTimeImmunity>= timeImmunity)
            {
                currentTimeImmunity = 0;
                immunity = false;
                immunityText.gameObject.SetActive(false);
            }
        }

        //Case when no one in the aera and point not catch
        if (((listPlayers[0]+listPlayers[1] == 0) || !NoneFromOtherTeam(teamInside)) && currentTimeToCatch > 0 && !objectifCatch)
        {
            currentTimeToCatch -= Time.deltaTime;
            timer.text = ((int)currentTimeToCatch).ToString();
            if (currentTimeToCatch <= 0)
            {
                teamInside = TeamScript.Team.none;
                SetColorTextMesh();
            }
        }

        if(objectifCatch && currentTimeToCatch < timeToCatch && (listPlayers[0] + listPlayers[1] == 0))
        {
            currentTimeToCatch += Time.deltaTime;
            timer.text = ((int)currentTimeToCatch).ToString();
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerScript>())
        {
            //DisplayListPlayers();
            InitTabTeams(other.GetComponent<PhotonView>().owner.GetPlayerTeam(), true);
            if (teamInside == TeamScript.Team.none || teamInside == other.GetComponent<PhotonView>().owner.GetPlayerTeam())
            {
                teamInside = other.GetComponent<PhotonView>().owner.GetPlayerTeam();
                SetColorTextMesh();
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerScript>())
        {
            if (teamInside == TeamScript.Team.none && (listPlayers[0] + listPlayers[1] == 1))
            {
                teamInside = other.GetComponent<PhotonView>().owner.GetPlayerTeam();
                SetColorTextMesh();
            }

            if(objectifCatch && !immunity && teamInside != other.GetComponent<PhotonView>().owner.GetPlayerTeam() && NoneFromOtherTeam(other.GetComponent<PhotonView>().owner.GetPlayerTeam()))
            {
                currentTimeToCatch -= Time.deltaTime;
                timer.text = ((int)currentTimeToCatch).ToString();
                if (currentTimeToCatch <= 0)
                {
                    teamInside = TeamScript.Team.none;
                    SetColorTextMesh();
                    objectifCatch = false;
                    canImmunity = true;
                }
            }

            if (teamInside == other.GetComponent<PhotonView>().owner.GetPlayerTeam() && NoneFromOtherTeam(teamInside))
            {
                if (currentTimeToCatch < timeToCatch)
                {
                    currentTimeToCatch += Time.deltaTime;// * listPlayer[] pour mettre un coef sur le nombre de personne sur la zone !
                    timer.text = ((int)currentTimeToCatch).ToString();
                }
                else
                {
                    //CHAMPION du MONDE !
                    objectifCatch = true;
                    if (canImmunity)
                    {
                        immunity = true;
                        canImmunity = false;
                        immunityText.gameObject.SetActive(true);
                    }
                }
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerScript>())
        {
            InitTabTeams(other.GetComponent<PhotonView>().owner.GetPlayerTeam(), false);
        }
    }

    private void SetColorTextMesh()
    {
        if (teamInside == TeamScript.Team.red)
            timer.color = Color.red;
        else if (teamInside == TeamScript.Team.blue)
            timer.color = Color.blue;
        else
            timer.color = Color.green;
    }

    private void InitTabTeams(TeamScript.Team team, bool entry)
    {
        if (team == TeamScript.Team.red)
        {
            if (entry)
                listPlayers[0] += 1;
            else
                listPlayers[0] -= 1;
        }
        else if (team == TeamScript.Team.blue)
        {
            if (entry)
                listPlayers[1] += 1;
            else
                listPlayers[1] -= 1;
        }
    }

    private void DisplayListPlayers()
    {
        Debug.LogError("l'equipe red a " + listPlayers[0] + " menbers");
        Debug.LogError("l'equipe blue a " + listPlayers[1] + " menbers");
    }

    private bool NoneFromOtherTeam(TeamScript.Team team)
    {
        if(team == TeamScript.Team.red)
        {
            if (listPlayers[1] != 0)
                return false;
            return true;
        }else if(team == TeamScript.Team.blue)
        {
            if (listPlayers[0] != 0)
                return false;
            return true;
        }
        return false;
    }
}
