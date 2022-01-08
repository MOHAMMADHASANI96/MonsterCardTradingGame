using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MonsterCardTradingGame.routs.Help
{
    public class NameSeparation
    {
        public String element_type { get; private set; }
        public String card_type { get; private set; }

        public NameSeparation(String name)
        {
            try
            {
                String[] split = Regex.Split(name, @"(?<!^)(?=[A-Z])");
                element_type = split[0];
                card_type = split[1];
            }
            catch (Exception)
            {
                element_type = null;
                card_type = name;
            } 
        }

    }
}
