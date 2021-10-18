using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour
{
    public GameObject ui;
    public Text upgradeCost;
    public Button upgradeButton;

    private Node target;
    public void SetTarget(Node _target)
    {
        if(target == _target)
        {
            ui.SetActive(false);
        }
        target = _target;
        transform.position = target.transform.position;
        if (!target.isUpgraded)
        {
            upgradeCost.text = "£" + target.turretBlueprint.upgradeCost;
        }
        else
        {
            upgradeCost.text = "MAX LEVEL";
            upgradeButton.interactable = false;
        }
        ui.SetActive(true);
    }
    public void Hide()
    {
        ui.SetActive(false);
    }
    public void Upgrade()
    {
        target.UpgradeTurret();
        BuildManager.instance.DeselectNode();
    }
    public void UpgradeCostText(int cost)
    {
        upgradeCost.text = cost.ToString();
    }
}
