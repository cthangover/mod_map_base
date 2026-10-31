using Cthangover.Core.Actions;
using Cthangover.Core.Mods;

namespace Cthangover.Mods.Map
{
    /// <summary>
    /// Wires the map subsystem into the kernel. On
    /// <c>OnModResourcesReady</c> it installs the zone-state hooks into
    /// <see cref="MapStateBridge"/> — export into the save's
    /// <c>__core__</c> slot, restore from a loaded save, and the
    /// new-game override reset — so the core save pipeline keeps working
    /// against the mod-owned <see cref="MapZoneService"/> singleton.
    /// <para/>
    /// It also fills the kernel slot <see cref="MapZoneServiceRegistry"/> with
    /// the scenario adapter (<c>ctx.Map</c> of the dialog's action context)
    /// and empties it when this mod is unloaded, so the dialog engine never
    /// names a map type and a game without the world map does not load it.
    /// </summary>
    public class MapModInitializer : IModInitializer, IModUnloadAware
    {
        /// <summary>
        /// Publishes the scenario adapter. Overwrites on purpose: after a reload
        /// the slot must point at this assembly's adapter, not at an unloaded one.
        /// </summary>
        public void OnModLoaded(string modId)
        {
            MapZoneServiceRegistry.Provider = new MapZoneServiceImpl();
        }

        /// <summary>Installs the zone-state hooks (first registration wins).</summary>
        public void OnModResourcesReady()
        {
            var service = MapZoneService.Instance;
            // Runs again after every mod reload; the service outlives it when map_base stays loaded.
            service.MarkStale();
            MapStateBridge.Register(service.ExportState, service.ImportState, service.ResetOverrides);
        }

        /// <summary>Empties the kernel slot if it still holds this assembly's adapter, so it does not pin the unloaded mod.</summary>
        public void OnModUnloading()
        {
            if (MapZoneServiceRegistry.Provider is MapZoneServiceImpl)
                MapZoneServiceRegistry.Provider = null;
        }

        /// <summary>Nothing to drop: the map mod holds no references into other mods.</summary>
        public void OnModUnloaded(string modId)
        {
        }
    }
}
