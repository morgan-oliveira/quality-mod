using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ModLoader.IO;
using Terraria.DataStructures;
using Microsoft.Xna.Framework.Graphics;

public class GlobalQuality : GlobalItem {
    public int quality;
    public override bool InstancePerEntity => true;
    protected override bool CloneNewInstances => true;
    public bool IsArmorBroken = false;
    // Checks if an item is broken
    public static bool IsBrokenItem(Item item) {
        if (item.GetGlobalItem<GlobalQuality>().quality < 40) {
            return true;
        } else return false;
    }
    // Assign the custom field quality to *this* item (the actual instance of an item)
    public override void SetDefaults(Item item)
    {
        if (item.damage > 0 || item.defense > 0) {
            item.GetGlobalItem<GlobalQuality>().quality = quality;
        }
    }
    // Tooltips
    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        ArmorPenalty(item);
        // Only add tooltip "Broken" if item is broken
        if (IsArmorBroken) {
            tooltips.Add(new TooltipLine(Mod, "Defense", $"{item.defense} defense") { OverrideColor = Color.Gray });
        }   
        if (IsBrokenItem(item)) {
            tooltips.Add(new TooltipLine(Mod, "broken", "Broken") { OverrideColor = Color.Gray });              
        } else  
        // Only add quality% tooltip if quality >= 40
        if (item.damage > 0 || item.defense > 0) {
            tooltips.Add(new TooltipLine(Mod, "quality", $"Quality: {quality}%") { OverrideColor = Color.BlueViolet });
        }
    }
    // Cannot use item if broken
    public override bool CanUseItem(Item item, Player player)
    {
        if (item.damage > 0 || item.defense > 0) {
            if (IsBrokenItem(item)) {
                return false;
            } 
        } else return true;
        return true;
    }
    // Cannot equip accessory if broken
    public override bool CanEquipAccessory(Item item, Player player, int slot, bool modded)
    {
        if (item.damage > 0 || item.defense > 0) {
            if (IsBrokenItem(item)) {
                return false;
            } 
        } else return true;
        return true;
    }
    public void ArmorPenalty(Item item) {
        if (item.bodySlot != -1 || item.bodySlot != 0 || item.bodySlot != -2 || item.accessory) {
            if (IsBrokenItem(item)) {
                item.defense = 0;
                IsArmorBroken = true;
            }
        }
    }

    // Assign random quality during item spawn (chests/drop/bags)
    public override void OnSpawn(Item item, IEntitySource source)
    {
        quality = Main.rand.Next(0, 100+1);
        ArmorPenalty(item);
    }
    // Assign random quality during creation (craft)
    public override void OnCreate(Item item, ItemCreationContext context)
    {
        quality = Main.rand.Next(0, 100+1);
        ArmorPenalty(item);
    }
    // Saving quality data with TagCompound object
    public override void SaveData(Item item, TagCompound tag)
    {
        tag["quality"] = quality;
    }
    // Loading quality data with TagCompound object
    public override void LoadData(Item item, TagCompound tag)
    {
        quality = tag.GetInt("quality");
    }

}
 