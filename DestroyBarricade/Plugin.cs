using Rocket.Core.Plugins;
using SDG.Unturned;
using System.Linq;

namespace BarricadeDamage
{
    public class Plugin : RocketPlugin<Config>
    {
    
        protected override void Load()
        {
             if (Configuration.Instance.barricadeSettings.enabled)
             {
                 var time = Configuration.Instance.barricadeSettings.minutesForDamage * 60;
                 InvokeRepeating("Damage", time, time);
             }
        }

        public void Damage()
        {
            ClearBarricade();              
        }


        private void ClearBarricade()
        {
            int i = 0;
            foreach (var region in BarricadeManager.regions)
                {               
                    foreach(var drop in region.drops.ToList())//ToList() - foreva
                    {
                        
                        Rocket.Core.Logging.Logger.Log("============" + drop.asset.name + "=====================================");
                        if(BarricadeManager.FindBarricadeByRootTransform(drop.model) != null)
                        {                           
                          if (Configuration.Instance.barricadeSettings.IsInWhiteList(drop.asset.id)) continue;
                        //Interactable2HP component = barricadeRegion.drops[(int)22].model.GetComponent<Interactable2HP>();                      
                          BarricadeManager.damage(drop.model, (drop.asset.health / 100) * Configuration.Instance.barricadeSettings.percentDamage, 1, false);                      
                        }
                        i++;
                    }
                } 
                Rocket.Core.Logging.Logger.Log("Damage to the barricades was committed, damaged " + i + " barricades");
        }


        protected override void Unload()
        {

            CancelInvoke("Damage");
        }
    }
}
