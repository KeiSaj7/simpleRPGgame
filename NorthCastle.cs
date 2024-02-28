namespace SimpleConsoleAppGame;

public class NorthCastle: Destination
{
    public NorthCastle()
    {
        this.Name = "North Castle";
        this.Stage = 1;
        this.Directions = new List<Destination>() {
            new Tavern(new Location(), new List<Item>()
            {
                new Item("DwarvenAxe",17,12,ItemType.Armor,null),
                new Item("CritBlessing", 20, 20, ItemType.CritBuff, null)
            })
        };
        this.Items = new List<Item>()
        {
            new Item("HealingPotion", 20, 2, ItemType.Potion, 100),
            new Item("GreaterHealingPotion", 35, 4, ItemType.Potion, 60),
            new Item("AncientSword", 22, 15, ItemType.Weapon, 10),
            new Item("AncientArmor", 12, 15, ItemType.Armor, 10),
            new Item("CritBlessing", 20, 20, ItemType.CritBuff, 10)
        };
        this.Enemies = new Dictionary<int, Enemy>()
        {
            { 1, new Enemy("Dark Servant",45,9,4,10) },
            { 2, new Enemy("Royal Paladin",55,8,5,10) },
            { 3, new Enemy("Fire Dragon", 60,11,6,20) },
            { 4, new Boss("Dark Knight",65,15,6,15,new Dictionary<string, Tuple<int, int>>{ { "Piercing Blow", new Tuple<int, int>(29, 30) } }) }
        };
    }

}