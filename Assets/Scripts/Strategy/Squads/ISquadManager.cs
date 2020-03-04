using SwordAndBored.GameData.Units;
using SwordAndBored.Strategy.ProceduralTerrain.Map.Grid.Cells;
using System.Collections.Generic;

namespace SwordAndBored.Strategy.Squads
{
    interface ISquadManager
    {
        SquadController SelectedSquad { get; }

        /// <summary>
        /// Create a squad of units on a give hex cell with a given name
        /// </summary>
        /// <param name="name">The name of the squad</param>
        /// <param name="units">The units in the squad</param>
        /// <param name="location">The place to place the squad</param>
        /// <returns></returns>
        SquadController DeploySquad(string name, List<IUnit> units, IHexGridCell location);

        /// <summary>
        /// Retrieves a squad of units and destroys the squad
        /// </summary>
        /// <param name="location">The cell the squad is on</param>
        /// <returns>The units that were in the squad</returns>
        IUnit[] CollectSquad(IHexGridCell location);
    }
}
