using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color hoverColour;
    public ParticleSystem buildFX;
    public ParticleSystem upgradeFX;

    [HideInInspector]
    public GameObject turret;
    [HideInInspector]
    public TurretBlueprint turretBlueprint;
    [HideInInspector]
    public bool isUpgraded;

    private Renderer rend;
    private Color startColour;

    BuildManager buildManager;
    

    private void Start()
    {
        rend = GetComponent<Renderer>();
        startColour = rend.material.color;
        buildManager = BuildManager.instance;
        
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject()) { return; }
        if (turret != null)
        {
            buildManager.SelectNode(this);            
            return;
        }

        if (!buildManager.canBuild) { return; }

        BuildTurret(buildManager.GetTurretToBuild());
        }

    void BuildTurret(TurretBlueprint blueprint)
    {
        if (PlayerStats.Money < blueprint.cost)
        {
            Debug.Log("Not enough money!");
            return;
        }
        PlayerStats.Money -= blueprint.cost;
        GameObject _turret = (GameObject)Instantiate(blueprint.prefab, transform.position, Quaternion.identity);
        turret = _turret;
        turretBlueprint = blueprint;
        Instantiate(buildFX, transform.position, Quaternion.identity);
        
        
    }

    public void UpgradeTurret()
    {
        if (PlayerStats.Money < turretBlueprint.upgradeCost)
        {
            Debug.Log("Not enough money to upgrade!");
            return;
        }
        PlayerStats.Money -= turretBlueprint.upgradeCost;
        
        //Gets rid of the old turret
        Destroy(turret);


        GameObject _turret = (GameObject)Instantiate(turretBlueprint.upgradedPrefab, transform.position, Quaternion.identity);
        turret = _turret;
        Instantiate(upgradeFX, transform.position, Quaternion.identity);
        isUpgraded = true;
    }

    private void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject()) { return; }

        if(!buildManager.canBuild) { return; }

        rend.material.color = hoverColour;
        if (turret != null||!buildManager.hasMoney)
        {
            rend.material.color = Color.red;            
            return;
        }
        
    }
    private void OnMouseExit()
    {
        rend.material.color = startColour;
    }
}
