using System;
using System.Collections;
using System.Collections.Generic;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

public class GlobalTier : GlobalItem {
    public override bool InstancePerEntity => true;
    protected override bool CloneNewInstances => true;
    public List<String> tiers = new List<String>() {"D", "C", "B", "A", "S", "SS", "SSS"};
    public String itemTier;
    int tierKey;
    public static bool NoQuality;
    
    public override void SetDefaults(Item item)
    {
        item.GetGlobalItem<GlobalTier>().itemTier = itemTier;
    }
    public override void OnSpawn(Item item, IEntitySource source)
    {
        tierKey = Main.rand.Next(0, tiers.Count);
    }
    public override void OnCreate(Item item, ItemCreationContext context)
    {
        tierKey = Main.rand.Next(0, tiers.Count);
    }


    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        if ((item.damage > 0) || (item.defense > 0)) {
                itemTier = tiers[tierKey];
                if (!GlobalQuality.SetBrokenItem(item)) {
                    tooltips.Add(new TooltipLine(Mod, "itemTier", $"Tier: {itemTier}") {OverrideColor = Color.Gold});
                }
        }
    }

    public override void SaveData(Item item, TagCompound tag)
    {
        tag["tierKey"] = tierKey;
    }

    public static bool IsTierD(Item item) {
        if (item.GetGlobalItem<GlobalTier>().itemTier == "D") {
            return true;
        } else return false;
    }

    public override void LoadData(Item item, TagCompound tag)
    {
        tierKey = tag.GetInt("tierKey");
    }


}