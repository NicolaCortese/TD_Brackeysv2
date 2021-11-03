using UnityEngine;
using UnityEngine.EventSystems;
using GameAnalyticsSDK;

public class Node : MonoBehaviour
{
    public Color hoverColour;
    public AudioClip buildSFX;
    public AudioClip sellSFX;
    

    [HideInInspector]
    public GameObject turret;
    [HideInInspector]
    public TurretBlueprint turretBlueprint;
    [HideInInspector]
    public bool isUpgraded;

    private Renderer rend;
    private Color startColour;

    BuildManager buildManager;
    AudioSource audiosource;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        startColour = rend.material.color;
        buildManager = BuildManager.instance;
        audiosource = GetComponent<AudioSource>();
        
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
        if (PlayerStats.Money < blueprint.cost){return;}
                
        GameAnalytics.NewResourceEvent(GAResourceFlowType.Sink, "Gold", blueprint.cost, "Turret", blueprint.prefab.name);
        
        

        PlayerStats.Money -= blueprint.cost;
        GameObject _turret = (GameObject)Instantiate(blueprint.prefab, transform.position, Quaternion.identity);
        turret = _turret;
        turretBlueprint = blueprint;
        Instantiate(buildManager.buildFX, transform.position, Quaternion.identity);
        audiosource.PlayOneShot(buildSFX);

    }

    public void UpgradeTurret()
    {
        if (PlayerStats.Money < turretBlueprint.upgradeCost){return;}

        GameAnalytics.NewResourceEvent(GAResourceFlowType.Sink, "Gold", turretBlueprint.upgradeCost, "Turret", turretBlueprint.upgradedPrefab.name);


        PlayerStats.Money -= turretBlueprint.upgradeCost;
        
        //Gets rid of the old turret
        Destroy(turret);


        GameObject _turret = (GameObject)Instantiate(turretBlueprint.upgradedPrefab, transform.position, Quaternion.identity);
        turret = _turret;
        Instantiate(buildManager.upgradeFX, transform.position, Quaternion.identity);
        audiosource.PlayOneShot(buildSFX);
        
        isUpgraded = true;
    }

    public void SellTurret()
    {
        if (isUpgraded)
        {
            PlayerStats.Money += turretBlueprint.upgradedSaleValue;
            GameAnalytics.NewResourceEvent(GAResourceFlowType.Source, "Gold", turretBlueprint.upgradedSaleValue, "Turret", turretBlueprint.upgradedPrefab.name);
        }
        else 
        { 
            PlayerStats.Money += turretBlueprint.saleValue;
            GameAnalytics.NewResourceEvent(GAResourceFlowType.Source, "Gold", turretBlueprint.saleValue, "Turret", turretBlueprint.prefab.name);
        }

        Instantiate(buildManager.sellFX, transform.position, Quaternion.identity);
        Instantiate(buildManager.sellDebrisFX, transform.position+new Vector3 (0f,2.5f,0f), Quaternion.identity);
        audiosource.PlayOneShot(sellSFX);
        Destroy(turret);
        turretBlueprint = null;
        isUpgraded = false;
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
