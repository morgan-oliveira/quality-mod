using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ModLoader.IO;

public class GlobalQuality : GlobalItem {
    public int quality;
    public override bool InstancePerEntity => true;
    protected override bool CloneNewInstances => true;

    public override void SetDefaults(Item item)
    {
        item.GetGlobalItem<GlobalQuality>().quality = quality;
    }

    public override void OnCreate(Item item, ItemCreationContext context)
    {
        quality = Main.rand.Next(40, 100+1);
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        if (item.damage > 0 || item.defense > 0) {
            tooltips.Add(new TooltipLine(Mod, "quality", $"Quality: {quality}%") { OverrideColor = Color.BlueViolet });
        }
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