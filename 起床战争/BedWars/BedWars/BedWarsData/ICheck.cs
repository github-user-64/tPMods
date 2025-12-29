using System;

namespace BedWars.BedWarsData
{
    internal interface ICheck
    {
        /// <exception cref="Exception"/>
        void Check(MapData mapData);
    }
}
