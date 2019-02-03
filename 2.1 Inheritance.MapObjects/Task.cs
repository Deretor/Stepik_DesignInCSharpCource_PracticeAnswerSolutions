using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inheritance.MapObjects
{
    public interface IOwnerable
    {
        int Owner { get; set; }
    }

    public interface IArmed
    {
        Army Army { get; set; }
    }

    public interface ILootable
    {
        Treasure Treasure { get; set; }
    }

    public class Dwelling: IOwnerable
    {
        public int Owner { get; set; }
    }

    public class Mine: IOwnerable, IArmed, ILootable
    {
        public int Owner { get; set; }
        public Army Army { get; set; }
        public Treasure Treasure { get; set; }
    }

    public class Creeps: IArmed, ILootable
    {
        public Army Army { get; set; }
        public Treasure Treasure { get; set; }
    }

    public class Wolfs: IArmed
    {
        public Army Army { get; set; }
    }

    public class ResourcePile: ILootable
    {
        public Treasure Treasure { get; set; }
    }

    public static class Interaction
    {
        public static bool Battle(Player player, IArmed enemy) {
            if (player == null) {
                throw new ArgumentException("Player can not be null");
            }
            if (enemy == null) {
                throw new ArgumentException("Player can not fight with null enemy");
            }
            var battleResult = player.CanBeat(enemy.Army);
            if (!battleResult) { player.Die(); }
            return battleResult;
        }

        public static void CalimOwnership(Player player, IOwnerable property) {

            if (property == null)
            {
                throw new ArgumentException("Player can not own null");
            }
            property.Owner = player?.Id ?? default(int);
        }
        
        public static void Make(Player player, object mapObject)
        {
            if (mapObject is IArmed) {
                var res  = Interaction.Battle(player, mapObject as IArmed);
                if (!res) return;
            }
            if (mapObject is ILootable) {
                player.Consume((mapObject as ILootable)?.Treasure ?? null);
            }
            if (mapObject is IOwnerable) {
                CalimOwnership(player, mapObject as IOwnerable);
            }
        }
    }
}

