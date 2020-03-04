using SwordAndBored.GameData.Units;
using SwordAndBored.Strategy.ProceduralTerrain.Map.Grid.Cells;

namespace SwordAndBored.Strategy.Squads
{
    interface ISquadManager
    {
        SquadController SelectedSquad { get; }

        /// <summary>
        /// Create a squad of units on a give hex cell
        /// </summary>
        /// <param name="units">The units in the squad</param>
        /// <param name="location">The cell to place the squad on</param>
        /// <returns>The squad game object</returns>
        SquadController DeploySquad(IUnit[] units, IHexGridCell location);

        /// <summary>
        /// Retrieves a squad of units and destroys the squad
        /// </summary>
        /// <param name="location">The cell the squad is on</param>
        /// <returns>The units that were in the squad</returns>
        IUnit[] CollectSquad(IHexGridCell location);
    }
}
