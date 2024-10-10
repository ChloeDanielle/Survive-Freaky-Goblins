using System;

[System.Serializable]
public class Skill
{
    public string skillName;           // Name of the skill
    public int skillLevel;             // Current skill level
    public int maxLevel = 3;           // Maximum skill level
    private Action onUpgradeAction;    // Action to perform on upgrade

    public Skill(string name, Action onUpgrade)
    {
        skillName = name;
        skillLevel = 1;
        onUpgradeAction = onUpgrade;
    }

    // Method to upgrade the skill
    public void Upgrade()
    {
        if (skillLevel < maxLevel)
        {
            skillLevel++;
            onUpgradeAction.Invoke();
        }
    }

    // Check if the skill is maxed out
    public bool IsMaxLevel()
    {
        return skillLevel >= maxLevel;
    }
}
