using UnityEngine;

public class shop : MonoBehaviour
{
    public TurretBlueprint standardTurret;
    public TurretBlueprint missileTurret;
    public TurretBlueprint laserTurret;
    BuildManager buildManager;

    private void Start()
    {
        buildManager = BuildManager.instance;
    }
    public void SelectStandardTurret()
    {
        buildManager.SelectTurretToBuild(standardTurret);
    }
    public void PurchaseMissileTurret()
    {
        
        buildManager.SelectTurretToBuild(missileTurret);
    }
    public void PurchaseLaserTurret()
    {
        
        buildManager.SelectTurretToBuild(laserTurret);
    }
}
