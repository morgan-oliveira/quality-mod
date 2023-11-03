using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ModLoader.IO;
using Terraria.DataStructures;

public class GlobalQuality : GlobalItem {
    public int quality;
    public override bool InstancePerEntity => true;
    protected override bool CloneNewInstances => true;
    public static bool SetBrokenItem(Item item) {
        if (item.GetGlobalItem<GlobalQuality>().quality < 40) {
            return true;
        } else return false;
    }
    public override void SetDefaults(Item item)
    {
        if (item.damage > 0 || item.defense > 0) {
            item.GetGlobalItem<GlobalQuality>().quality = quality;
        }
    }


    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        if (item.damage > 0 || item.defense > 0) {
            if (SetBrokenItem(item)) {
                tooltips.Add(new TooltipLine(Mod, "broken", "Broken") { OverrideColor = Color.Gray });
                if (item.GetGlobalItem<GlobalTier>().itemTier == "D") {
                    GlobalTier.NoQuality = true;
                }
            } else 
            tooltips.Add(new TooltipLine(Mod, "quality", $"Quality: {quality}%") { OverrideColor = Color.BlueViolet });
        }
        
    }
    // Cannot use item if broken
    public override bool CanUseItem(Item item, Player player)
    {
        if (item.damage > 0 || item.defense > 0) {
            if (SetBrokenItem(item)) {
                return false;
            } 
        } else return true;
        return true;
    }
    // Cannot equit accessory if broken
    public override bool CanEquipAccessory(Item item, Player player, int slot, bool modded)
    {
        if (item.damage > 0 || item.defense > 0) {
            if (SetBrokenItem(item)) {
                return false;
            } 
        } else return true;
        return true;
    }
    public override void OnSpawn(Item item, IEntitySource source)
    {
        quality = Main.rand.Next(0, 100+1);
    }
    public override void OnCreate(Item item, ItemCreationContext context)
    {
        quality = Main.rand.Next(0, 100+1);
    }

    public override void SaveData(Item item, TagCompound tag)
    {
        tag["quality"] = quality;
    }

    public override void LoadData(Item item, TagCompound tag)
    {
        quality = tag.GetInt("quality");
    }

} 